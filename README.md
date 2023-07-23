# Simple Rust Rat

## Limitations

The client is still in development.

This version only supports local client-server connections, for dangerous reasons. If you want to use it remotely, you have to modify the client and server code
Moreover, it only handles one client

Also, there are some bugs in the LSASS dumper, 


## Features

- get username
- local ip
- check defender
- check executable path
- list directories in given path
- type file content in given path
- hide executable file (todo)
- destroy executable file and uninstall (todo)
- GET file from http
- POST file to http (todo)
- enumerate hardware specs (todo)
- search process by name
- kill process by name
- tasklist
- lsass dump
- paranoid mode (todo)
- multiple clients


## Server

![image](https://github.com/m3f157O/rust_rat/assets/79704302/8172a2b1-b6bd-4c4e-9539-9694ec11a781)

GUI is pretty self explanatory

## Client

The architecture of the client is inspired by FIN7 Carbanak malware, relying on a heavy and redundant inter-process communication for evasion,

A number of Rust crates has been used, as they unexpectedly decrease the detection potential (empirical experiments)

