#if __ANDROID__
#include "../Android/AssetFile.h"
#include "../Header/DebugLog.h"
namespace LWN
{
	AAssetManager*  AssetFile::s_pAssetMgr = NULL;
	AssetFile::AssetFile()
		:m_szFileName(NULL), m_uSize(0)
	{
	}

	AssetFile::~AssetFile()
	{

	}

	bool AssetFile::open(STRING szFileName)
	{
	    //alog("----AssetFile::open-----szFileName=%s",szFileName);
		AAsset *pAsset = AAssetManager_open(s_pAssetMgr, szFileName, AASSET_MODE_UNKNOWN);
		//alog("----AssetFile::open-----(pAsset == NULL)=%d",(pAsset == NULL));
		if (pAsset == NULL)
			return false;
        //alog("----AssetFile::open-----pAsset OK");
		m_uSize = AAsset_getLength(pAsset);
		AAsset_close(pAsset);
		m_szFileName = szFileName;
		return true;
	}

	bool AssetFile::read(VOID* buffer, UINT dataSize)
	{
	    //alog("----AssetFile::read-----dataSize=%d",dataSize);
		AAsset* pAsset = AAssetManager_open(s_pAssetMgr, m_szFileName, AASSET_MODE_UNKNOWN);
		//alog("----AssetFile::read-----(pAsset == NULL)=%d",(pAsset == NULL));
		if (pAsset == NULL)
			return false;
        //alog("----AssetFile::read-----pAsset OK");
		off_t t_offset = AAsset_seek(pAsset, m_uOffset, 0);
		//alog("----AssetFile::read-----AAsset_seek");
		int iRet = AAsset_read(pAsset, buffer, dataSize);
		//alog("----AssetFile::read-----iRet=%d",iRet);
		if (iRet <= 0)
		{
		    //alog("----AssetFile::read-----return false;");
			return false;
		}
		//alog("----AssetFile::read-----iRet > 0");
		AAsset_close(pAsset);
		//alog("----AssetFile::read-----return buffer;");
		return buffer;
		return true;
	}

	UINT AssetFile::length()
	{
		return m_uSize;
	}
}
#endif
/*
用于操作asset，其中包含了各种对asset文件的操作方法
//需要引入的头文件
#include <android/asset_manager_jni.h>

基本步骤
AAssetManager* AAssetManager_fromJava(JNIEnv* env, jobject assetManager);
//open eg:
AAsset* AAssetManager_open(AAssetManager* mgr, const char* filename, int mode);
//operate eg:
int AAsset_read(AAsset* asset, void* buf, size_t count);
//close eg:
void AAsset_close(AAsset* asset);

简单使用，有2处不明白的地方。
1.利用文件描述符读取内容始终不对
2.api中采用64位长度获取大小的值很奇怪，没看懂
有知道的兄弟麻烦告诉我下
AAssetManager *aAssetManager=AAssetManager_fromJava(env,assetManager);
AAsset *aAsset=AAssetManager_open(aAssetManager,"asset_test.txt",AASSET_MODE_UNKNOWN);
//实际测试发现返回的数据末尾没有\0,如果需要打印信息自己补
//获取文件的全部内容
char *buff=(char*)AAsset_getBuffer(aAsset);
off_t len=AAsset_getLength(aAsset);
LOGI("%s",buff);
LOGI("%d",len);
char *actual_buff=malloc(sizeof(char)*len+1);
actual_buff[len]='\0';
memcpy(actual_buff,buff,len);
LOGI("%s",actual_buff);

//读取
char *read_buff=malloc(sizeof(char)*5+1);
read_buff[5]='\0';
AAsset_read(aAsset,read_buff,5);
LOGI("%s",read_buff);
//剩余数据长度
off_t remain=AAsset_getRemainingLength(aAsset);
LOGI("%d",remain);

//seek
AAsset_seek(aAsset,0,SEEK_SET);
remain=AAsset_getRemainingLength(aAsset);
LOGI("%d",remain);

//这一部分读的内容始终不对，不知道为什么
//    //file descriptor
//    off_t start,lens;
//    int fd=AAsset_openFileDescriptor(aAsset,&start,&lens);
//    FILE *fp=fdopen(fd,"r");
//    char *d_buff=malloc(sizeof(char)*lens+1);
//    memset(d_buff,0,lens+1);
//    fread(d_buff,1,lens+1,fp);
//    fclose(fp);
//    LOGI("%s",d_buff);
//    free(d_buff);


//    这个64返回的值很奇怪不知道为什么
//    off64_t len64=AAsset_getLength64(aAsset);
//    LOGI("%d",len64);
AAsset_close(aAsset);

//遍历文件
AAssetDir *aAssetDir=AAssetManager_openDir(aAssetManager,"test");
char *file_list;
do{
file_list=AAssetDir_getNextFileName(aAssetDir);
if(file_list)
LOGI("%s",file_list);
}while (file_list);
*/

