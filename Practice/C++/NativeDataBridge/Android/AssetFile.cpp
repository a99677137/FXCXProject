#if __ANDROID__
#include "../Android/AssetFile.h"
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
		AAsset *pAsset = AAssetManager_open(s_pAssetMgr, szFileName, AASSET_MODE_UNKNOWN);
		if (pAsset == NULL)
			return false;
		m_uSize = AAsset_getLength(pAsset);
		AAsset_close(pAsset);
		m_szFileName = szFileName;
		return true;
	}

	bool AssetFile::read(VOID* buffer, UINT dataSize)
	{
		AAsset* pAsset = AAssetManager_open(s_pAssetMgr, m_szFileName, AASSET_MODE_UNKNOWN);
		if (pAsset == NULL)
			return false;

		off_t t_offset = AAsset_seek(pAsset, m_uOffset, 0);
		int iRet = AAsset_read(pAsset, buffer, dataSize);
		if (iRet <= 0)
		{
			return false;
		}
		AAsset_close(pAsset);
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
���ڲ���asset�����а����˸��ֶ�asset�ļ��Ĳ�������
//��Ҫ�����ͷ�ļ�
#include <android/asset_manager_jni.h>

��������
AAssetManager* AAssetManager_fromJava(JNIEnv* env, jobject assetManager);
//open eg:
AAsset* AAssetManager_open(AAssetManager* mgr, const char* filename, int mode);
//operate eg:
int AAsset_read(AAsset* asset, void* buf, size_t count);
//close eg:
void AAsset_close(AAsset* asset);

��ʹ�ã���2�������׵ĵط���
1.�����ļ���������ȡ����ʼ�ղ���
2.api�в���64λ���Ȼ�ȡ��С��ֵ����֣�û����
��֪�����ֵ��鷳��������
AAssetManager *aAssetManager=AAssetManager_fromJava(env,assetManager);
AAsset *aAsset=AAssetManager_open(aAssetManager,"asset_test.txt",AASSET_MODE_UNKNOWN);
//ʵ�ʲ��Է��ַ��ص�����ĩβû��\0,�����Ҫ��ӡ��Ϣ�Լ���
//��ȡ�ļ���ȫ������
char *buff=(char*)AAsset_getBuffer(aAsset);
off_t len=AAsset_getLength(aAsset);
LOGI("%s",buff);
LOGI("%d",len);
char *actual_buff=malloc(sizeof(char)*len+1);
actual_buff[len]='\0';
memcpy(actual_buff,buff,len);
LOGI("%s",actual_buff);

//��ȡ
char *read_buff=malloc(sizeof(char)*5+1);
read_buff[5]='\0';
AAsset_read(aAsset,read_buff,5);
LOGI("%s",read_buff);
//ʣ�����ݳ���
off_t remain=AAsset_getRemainingLength(aAsset);
LOGI("%d",remain);

//seek
AAsset_seek(aAsset,0,SEEK_SET);
remain=AAsset_getRemainingLength(aAsset);
LOGI("%d",remain);

//��һ���ֶ�������ʼ�ղ��ԣ���֪��Ϊʲô
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


//    ���64���ص�ֵ����ֲ�֪��Ϊʲô
//    off64_t len64=AAsset_getLength64(aAsset);
//    LOGI("%d",len64);
AAsset_close(aAsset);

//�����ļ�
AAssetDir *aAssetDir=AAssetManager_openDir(aAssetManager,"test");
char *file_list;
do{
file_list=AAssetDir_getNextFileName(aAssetDir);
if(file_list)
LOGI("%s",file_list);
}while (file_list);
*/

