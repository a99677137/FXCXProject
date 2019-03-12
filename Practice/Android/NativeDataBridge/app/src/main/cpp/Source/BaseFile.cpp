#include "../Header/BaseFile.h"
namespace AmQ

{
	BaseFile::BaseFile()
		:fp(NULL)
	{
	}

	BaseFile::~BaseFile()
	{
		if(fp!=NULL)
		{ 
			fclose(fp);
			fp = NULL;
		}
	}

	bool BaseFile::open(STRING szFileName)
	{
		if (fp != NULL) return false;
		fp = fopen(szFileName, "rb");
		if (fp != NULL)
		{
			Valid = true;
			return true;
		}
		else
		{
			Valid = false;
			return false;
		}
	}
	bool BaseFile::read(VOID* buffer, UINT dataSize)
	{
		fseek(fp, m_uOffset, SEEK_SET);
		fread(buffer, dataSize, 1, fp);
		return true;
	}

	UINT BaseFile::length()
	{
		fseek(fp, 0, SEEK_END);
		return ftell(fp);
	}
}



