package com.lwn.lwntest.normal;
import android.util.Log;

public class TlbbLog {
	private static boolean _isOpenLog = true;
	private static boolean _isOpenErrorLog = true;
	private static String Tag = "LwnTest";
	
	public static void SetDebug(boolean isDebug){
		_isOpenLog = isDebug;
		Log.d(Tag,"----------------------TlbbDebug = " + _isOpenLog);
	}
	
	public static void d(String msg){
		if(!_isOpenLog)
			return;
		Log.d(Tag,msg);
	}
	
	public static void d(String tag,String msg){
		if(!_isOpenLog)
			return;
		Log.d(tag,msg);
	}
	
	public static void i(String msg){
		if(!_isOpenLog)
			return;
		Log.i(Tag,msg);
	}
	
	public static void i(String tag,String msg){
		if(!_isOpenLog)
			return;
		Log.i(tag,msg);
	}
	
	public static void w(String msg){
		if(!_isOpenLog)
			return;
		Log.w(Tag,msg);
	}
	
	public static void w(String tag,String msg){
		if(!_isOpenLog)
			return;
		Log.w(tag,msg);
	}
	
	public static void e(String msg){
		if(!_isOpenErrorLog)
			return;
		Log.e(Tag,msg);
	}
	
	public static void e(String tag,String msg){
		if(!_isOpenErrorLog)
			return;
		Log.e(tag,msg);
	}
	
}
