
#include <stdio.h>
using namespace std;

#include <string>
#include <Vector>
#include <iostream>
#include <io.h>
#include "LwnTest.h"
int main(void) {

	char * fbspath = "C:\\Work\\TLBB_Main_U5\\Client\\FlatBufferOutputResult\\Generated\\GeneratedAllTable\\OriginalFbs";
	char * jsonpath = "C:\\Work\\TLBB_Main_U5\\Client\\FlatBufferOutputResult\\Generated\\GeneratedAllTable\\OriginalJson";
	vector<string> fbsfiles;
	vector<string> jsonfiles;
	getFiles(fbspath, fbsfiles);
	getFiles(jsonpath, jsonfiles);

	for (int i = 0; i < fbsfiles.size(); i++) {
		cout << fbsfiles[i].c_str() << endl;
		cout << jsonfiles[i].c_str() << endl;
	}
	int tmp = 0;
	cin >> tmp;
	return 0;
}

void getFiles(string path, vector<string>& files)
{
	//�ļ����  
	long   hFile = 0;
	//�ļ���Ϣ  
	struct _finddata_t fileinfo;
	string p;
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
	{
		do
		{
			//�����Ŀ¼,����֮  
			//�������,�����б�  
			if ((fileinfo.attrib &  _A_SUBDIR))
			{
				if (strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0)
					getFiles(p.assign(path).append("\\").append(fileinfo.name), files);
			}
			else
			{
				files.push_back(p.assign(path).append("\\").append(fileinfo.name));
			}
		} while (_findnext(hFile, &fileinfo) == 0);
		_findclose(hFile);
	}
}