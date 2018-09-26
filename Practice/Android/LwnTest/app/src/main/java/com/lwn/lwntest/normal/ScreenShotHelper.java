package com.lwn.lwntest.normal;

import android.annotation.SuppressLint;
import android.database.ContentObserver;
import android.database.Cursor;
import android.net.Uri;
import android.os.Handler;
import android.os.HandlerThread;
import android.provider.MediaStore;
import android.util.Log;
import android.database.ContentObserver;
import android.database.Cursor;
import android.net.Uri;
import android.os.Handler;
import android.os.HandlerThread;
import android.provider.MediaStore;
import android.util.Log;
import android.widget.Toast;

import com.lwn.lwntest.MainActivity;


public class ScreenShotHelper {
	
	private static String TAG="Lwn";
	
	
	private static ScreenShotHelper _instance = null;
	
	public static ScreenShotHelper GetInstance(){
		if(_instance == null){
			_instance = new ScreenShotHelper();
		}
		return _instance;
	}
	
	 private static final String[] KEYWORDS = {
         "screenshot", "screen_shot", "screen-shot", "screen shot",
         "screencapture", "screen_capture", "screen-capture", "screen capture",
         "screencap", "screen_cap", "screen-cap", "screen cap"
	 };
	 
	 /** 读取媒体数据库时需要读取的列 */
    private static final String[] MEDIA_PROJECTIONS =  {
            MediaStore.Images.ImageColumns.DATA,
            MediaStore.Images.ImageColumns.DATE_TAKEN,
    };
	
    /** 内部存储器内容观察者 */
    private  MediaContentObserver  mInternalObserver;

    /** 外部存储器内容观察者 */
    private  MediaContentObserver  mExternalObserver;

    private  HandlerThread mHandlerThread;
    private  Handler mHandler;
    
    public void OnCreate4CutScreen(){
    	if(MainActivity.CurActivity == null){
    		return;
    	}
    	mHandlerThread = new HandlerThread("Screenshot_Observer");
        mHandlerThread.start();
        mHandler = new Handler(mHandlerThread.getLooper());

        // 初始化
        mInternalObserver = new MediaContentObserver(MediaStore.Images.Media.INTERNAL_CONTENT_URI, mHandler);
        
        mExternalObserver = new MediaContentObserver(MediaStore.Images.Media.EXTERNAL_CONTENT_URI, mHandler);

        // 添加监听
        MainActivity.CurActivity.getContentResolver().registerContentObserver(
            MediaStore.Images.Media.INTERNAL_CONTENT_URI,
            false,
            mInternalObserver
        );
        MainActivity.CurActivity.getContentResolver().registerContentObserver(
            MediaStore.Images.Media.EXTERNAL_CONTENT_URI,
            false,
            mExternalObserver
        );
    	
    }
    
    
    private void handleMediaContentChange(Uri contentUri) {
        Cursor cursor = null;
        try {
            // 数据改变时查询数据库中最后加入的一条数据
            cursor = MainActivity.CurActivity.getContentResolver().query(
                    contentUri,
                    MEDIA_PROJECTIONS,
                    null,
                    null,
                    MediaStore.Images.ImageColumns.DATE_ADDED + " desc limit 1"
            );

            if (cursor == null) {
                return;
            }
            if (!cursor.moveToFirst()) {
                return;
            }

            // 获取各列的索引
            int dataIndex = cursor.getColumnIndex(MediaStore.Images.ImageColumns.DATA);
            int dateTakenIndex = cursor.getColumnIndex(MediaStore.Images.ImageColumns.DATE_TAKEN);

            // 获取行数据
            String data = cursor.getString(dataIndex);
            long dateTaken = cursor.getLong(dateTakenIndex);

            // 处理获取到的第一行数据
            handleMediaRowData(data, dateTaken);

        } catch (Exception e) {
            e.printStackTrace();

        } finally {
            if (cursor != null && !cursor.isClosed()) {
                cursor.close();
            }
        }
    }
    
    
    /**
     * 处理监听到的资源
     */
    private void handleMediaRowData(String data, long dateTaken) {
        if (checkScreenShot(data, dateTaken)) {
            Log.d(TAG, data + " " + dateTaken);
            //MainActivity.ShowSysDialog("LwnTest", "ScreenShot!!!!", "OK", "Cancel");
            Toast.makeText(MainActivity.CurActivity, "ScreenShot!!!", 10).show();
        } else {
            Log.d(TAG, "Not screenshot event");
        }
    }
    
    /**
     * 判断是否是截屏
     */
    private boolean checkScreenShot(String data, long dateTaken) {
    	Log.e(TAG, "******************* checkScreenShot-> data = " + data);
    	Log.e(TAG, "******************* checkScreenShot-> dateTaken = " + dateTaken);
    	Log.e(TAG, "******************* checkScreenShot-> currentTimeMillis = " + System.currentTimeMillis());
        data = data.toLowerCase();
        if(System.currentTimeMillis() - dateTaken >(10*1000)){
        	return false;
        }
        
        // 判断图片路径是否含有指定的关键字之一, 如果有, 则认为当前截屏了
        for (String keyWork : KEYWORDS) {
            if (data.contains(keyWork)) {
                return true;
            }
        }
        return false;
    }
    
    
    /**
     * 媒体内容观察者(观察媒体数据库的改变)
     */
    private class MediaContentObserver extends ContentObserver {

        private Uri mContentUri;

        public MediaContentObserver(Uri contentUri, Handler handler) {
            super(handler);
            mContentUri = contentUri;
        }

        @Override
        public void onChange(boolean selfChange) {
            super.onChange(selfChange);
            Log.e(TAG, "******************************************"+mContentUri.toString());
            handleMediaContentChange(mContentUri);
        }
    }
    
    
    public void OnDestory(){
    	if(MainActivity.CurActivity == null){
    		return;
    	}
    	// 注销监听
        MainActivity.CurActivity.getContentResolver().unregisterContentObserver(mInternalObserver);
        MainActivity.CurActivity.getContentResolver().unregisterContentObserver(mExternalObserver);
    }
}
