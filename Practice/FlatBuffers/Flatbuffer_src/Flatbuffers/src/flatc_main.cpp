/*
 * Copyright 2017 Google Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


#include "flatbuffers/flatc.h"
#include <iostream>
#include <io.h>
#include <string>
#include <vector>
#include "flatbuffers/lwn_debug.h"



static const char *g_program_name = nullptr;

static void Warn(const flatbuffers::FlatCompiler *flatc,
                 const std::string &warn, bool show_exe_name) {
  (void)flatc;
  if (show_exe_name) { printf("%s: ", g_program_name); }
  printf("warning: %s\n", warn.c_str());
}

static void Error(const flatbuffers::FlatCompiler *flatc,
                  const std::string &err, bool usage, bool show_exe_name) {
  if (show_exe_name) { printf("%s: ", g_program_name); }
  printf("error: %s\n", err.c_str());
  if (usage) { printf("%s", flatc->GetUsageString(g_program_name).c_str()); }
  exit(1);
}


//int main(int argc, const char *arg[]) {
int main_origin(int argc,  char **argv) {

  g_program_name = argv[0];

  const flatbuffers::FlatCompiler::Generator generators[] = {
    { flatbuffers::GenerateBinary, "-b", "--binary", "binary", false, nullptr,
      flatbuffers::IDLOptions::kBinary,
      "Generate wire format binaries for any data definitions",
      flatbuffers::BinaryMakeRule },
    { flatbuffers::GenerateTextFile, "-t", "--json", "text", false, nullptr,
      flatbuffers::IDLOptions::kJson,
      "Generate text output for any data definitions",
      flatbuffers::TextMakeRule },
    { flatbuffers::GenerateCPP, "-c", "--cpp", "C++", true,
      flatbuffers::GenerateCppGRPC, flatbuffers::IDLOptions::kCpp,
      "Generate C++ headers for tables/structs", flatbuffers::CPPMakeRule },
    { flatbuffers::GenerateGo, "-g", "--go", "Go", true,
      flatbuffers::GenerateGoGRPC, flatbuffers::IDLOptions::kGo,
      "Generate Go files for tables/structs", flatbuffers::GeneralMakeRule },
    { flatbuffers::GenerateGeneral, "-j", "--java", "Java", true,
      flatbuffers::GenerateJavaGRPC, flatbuffers::IDLOptions::kJava,
      "Generate Java classes for tables/structs",
      flatbuffers::GeneralMakeRule },
    { flatbuffers::GenerateJS, "-s", "--js", "JavaScript", true, nullptr,
      flatbuffers::IDLOptions::kJs,
      "Generate JavaScript code for tables/structs", flatbuffers::JSMakeRule },
    { flatbuffers::GenerateJS, "-T", "--ts", "TypeScript", true, nullptr,
      flatbuffers::IDLOptions::kTs,
      "Generate TypeScript code for tables/structs", flatbuffers::JSMakeRule },
    { flatbuffers::GenerateGeneral, "-n", "--csharp", "C#", true, nullptr,
      flatbuffers::IDLOptions::kCSharp,
      "Generate C# classes for tables/structs", flatbuffers::GeneralMakeRule },
    { flatbuffers::GeneratePython, "-p", "--python", "Python", true, nullptr,
      flatbuffers::IDLOptions::kPython,
      "Generate Python files for tables/structs",
      flatbuffers::GeneralMakeRule },
    { flatbuffers::GeneratePhp, nullptr, "--php", "PHP", true, nullptr,
      flatbuffers::IDLOptions::kPhp, "Generate PHP files for tables/structs",
      flatbuffers::GeneralMakeRule },
    { flatbuffers::GenerateJsonSchema, nullptr, "--jsonschema", "JsonSchema",
      true, nullptr, flatbuffers::IDLOptions::kJsonSchema,
      "Generate Json schema", flatbuffers::GeneralMakeRule },
  };

  flatbuffers::FlatCompiler::InitParams params;
  params.generators = generators;
  params.num_generators = sizeof(generators) / sizeof(generators[0]);
  params.warn_fn = Warn;
  params.error_fn = Error;

  flatbuffers::FlatCompiler flatc(params);
#ifdef LwnDebug
  return flatc.Compile(argc , argv);
#else
  return flatc.Compile(argc - 1, argv + 1);
#endif
}



#ifdef Lwn

void getFiles(std::string path, std::vector<std::string>& files)
{
	//文件句柄  
	long   hFile = 0;
	//文件信息
	struct _finddata_t fileinfo;
	std::string p;
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
	{
		do
		{
			//如果是目录,迭代之  
			//如果不是,加入列表  
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



int main(int argc, const char *argv[]) {

#ifdef LwnDebug
	argc = 5;
	const char* test[5] = { "-n","-o",
		"E:\\FXCX\\FXCX\\FB_Output\\cs",
		"E:\\FXCX\\FXCX\\FB_Output\\fbs" ,
		"E:\\FXCX\\FXCX\\FB_Output\\json" };
	argv = test;

	for (int argi = 0; argi < argc; argi++) {
		std::string arg = argv[argi];
		printf(" %s\n", arg.c_str());
	}
#endif

	std::vector<std::string> jsonFileNames;
	std::vector<std::string> fbsFileNames;
	std::map<std::string, std::string> fbsDirMap;

	int isCS = 0;

	for (int argi = 0; argi < argc; argi++) {
		std::string arg = argv[argi];

		if (arg[0] == '-' && arg[1] == 'n') {
			isCS = 1;
		}
		if (arg[0] == '-') {
		}
		else {
			std::vector<std::string> files;
			getFiles(arg, files);
			int len = strlen(argv[argi]);
			if (files.size() > 0) {
				int len = strlen(argv[argi]);
				if (files.size() > 0)
				{
					for (size_t i = 0; i < files.size(); ++i)
					{
						std::string extName = flatbuffers::GetExtension(files[i]);
						if (extName == "fbs")
						{
							fbsFileNames.push_back(files[i]);
							fbsDirMap.insert(std::pair<std::string, std::string>(files[i], files[i].substr(len, files[i].find_last_of('\\') - len)));
						}
						else if (extName == "json")
						{
							jsonFileNames.push_back(files[i]);
						}
					}//for
				}
			}

		}
	}//for

	if (fbsFileNames.size() <= 0 || jsonFileNames.size() <= 0 || fbsFileNames.size() != jsonFileNames.size()) {
		return 0;
	}
	std::sort(fbsFileNames.begin(), fbsFileNames.end());
	std::sort(jsonFileNames.begin(), jsonFileNames.end());
#ifdef LwnDebug
	char** newArgv = new char*[5];
	newArgv[0] = (char*)argv[0];//-n
	newArgv[1] = (char*)argv[1];//-o
	for (size_t i = 0; i < fbsFileNames.size(); ++i)
	{
		std::string path = (char*)argv[2];//根目录
		if (isCS != 1) {
			path += fbsDirMap[fbsFileNames[i]];//目标目录
		}
		newArgv[2] = (char*)path.c_str();
		newArgv[3] = (char*)fbsFileNames[i].c_str();
		newArgv[4] = (char*)jsonFileNames[i].c_str();
		main_origin(argc, newArgv);
	}
#else
	char** newArgv = new char*[6];
	newArgv[0] = (char*)argv[0];//flatc
	newArgv[1] = (char*)argv[1];//-n
	newArgv[2] = (char*)argv[2];//-o
	for (size_t i = 0; i < fbsFileNames.size(); ++i)
	{
		std::string path = (char*)argv[3];//根目录
		if (isCS != 1) {
			path += fbsDirMap[fbsFileNames[i]];//目标目录
		}
		newArgv[3] = (char*)path.c_str();
		newArgv[4] = (char*)fbsFileNames[i].c_str();
		newArgv[5] = (char*)jsonFileNames[i].c_str();
		main_origin(argc, newArgv);
	}
#endif

	return 0;
}


#endif