#pragma comment(lib,"Ws2_32.lib")
#include<WinSock2.h>
#include<iostream>
#include<WS2tcpip.h>

SOCKET Connect;
SOCKET *Connections;
SOCKET Listen;

int client_num = 0;

void send_to_client(int id);
bool  is_file(char* str);
bool is_closed(char *buffer);
const int max_size = 100000;
char file[max_size];
char current_copy[max_size];

int main()
{
	WSAData data;
	WORD version = MAKEWORD(2, 2);
	int result = WSAStartup(version, &data);
	if (result != 0)
	{
		return 0;
	}
	struct addrinfo hints;
	struct addrinfo *res;
	Connections = (SOCKET*)calloc(64, sizeof(SOCKET));
	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_flags = AI_PASSIVE;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	getaddrinfo(NULL, "7770", &hints, &res);
	Listen = socket(res->ai_family, res->ai_socktype, res->ai_protocol);
	bind(Listen, res->ai_addr, res->ai_addrlen);
	listen(Listen, SOMAXCONN);
	freeaddrinfo(res);
	printf("Started server....");
	char *m_connect = new char[10];
	for (;; Sleep(75))
	{
		if (Connect = accept(Listen, NULL, NULL))
		{
			printf("client connected...\n");
			Connections[client_num] = Connect;
			if (is_file(file))
			{
				send(Connections[client_num], file, max_size, NULL);
			}
			int copy = client_num;
			int pos = 0;
			while ((copy / 10) > 0)
			{
				m_connect[pos] = (copy / 10) + '0';
				copy = copy / 10;
				pos++;
			}
			m_connect[pos] = copy + '0'; pos++;
			m_connect[pos] = ';'; pos++; m_connect[pos] = ';'; pos++; m_connect[pos] = ';'; pos++;
			m_connect[pos] = '-'; pos++; m_connect[pos] = '5';
			send(Connections[client_num], m_connect, strlen(m_connect), NULL);
			send(Connections[client_num], current_copy, max_size, NULL);
			client_num++;
			CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)send_to_client, (LPVOID)(client_num - 1), NULL, NULL);
		}
	}
	return 1;
}


void send_to_client(int id)
{
	char *buffer = new char[max_size];
	for (;; Sleep(75))
	{
		memset(buffer, 0, sizeof(buffer));
		if (recv(Connections[id], buffer, max_size, NULL))
		{
			if (is_file(buffer)) { for (int i = 0; i < max_size; i++)
				file[i] = buffer[i]; }
			if (is_closed(buffer)) 
			{ 
				closesocket(Connections[id]);
				break;
			}
			for (int i = 0; i < max_size; i++)
				current_copy[i] = buffer[i];
			for (int i = 0; i <= client_num; i++)
			{
				if (i != id)
				{
					send(Connections[i], buffer, max_size, NULL);
				}
			}
		}
	}
	delete buffer;
	ExitThread(id);
}

bool is_closed(char *buffer)
{
	if (buffer[0] == ';' && buffer[1] == ';' && buffer[2] == ';' && buffer[3] == ';' && buffer[4] == '-' && buffer[5] == '2' && buffer[6] == ';')
	{
		return true;
	}
	else return false;
}

bool  is_file(char* str)
{
	char *f=strstr(str, ";;;f");
	if (f != NULL)
		return true;
	else return false;
}