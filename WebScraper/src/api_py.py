'''
Created on Jan 30, 2021

@author: Jeremy
'''
import socket

def make_request(url):
    print('User requesting HTML: ' + url)
    HOST = url
    PORT = 80
    
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_address = (HOST, PORT)
    client_socket.connect(server_address)
    
    request_header = b'GET / HTTP/1.0\r\nHost: www.google.com\r\n\r\n'
    client_socket.sendall(request_header)
    
    response = ''
    while True:
        recv = client_socket.recv(1024)
        if not recv:
            break
        response += str(recv)
    print('response: %s' % response)
    client_socket.close()
    return response