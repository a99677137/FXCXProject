package com.lwn.lwntest.normal;

import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import org.json.JSONException;
import org.json.JSONObject;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.IBinder;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.lwn.lwntest.MainActivity;


public class NotificationService extends Service {

	public static Handler handler;
	static int pushId;
	int tag;
	private static SharedPreferences share;
	private static String PUSH_TAG = "PUSH_TAG";
	private static final String PUSH_KEY = "PUSH_KEY";
	private static final String PUSH_ID = "PUSH_ID";

	public static void savePushInfo(String key, String value) {
		share.edit().putString(PUSH_KEY + key, value).commit();
//		Log.e("Lwn-----------", "---------------Key = " +key + "   Value = " + value);
	}

	public static String getPushInfo(String key) {
		return share.getString(key, "");
	}

	public static void deleteInfo(String key) {
		SharedPreferences.Editor editor = share.edit();
		editor.remove(key);
		editor.commit();
	}

	public static void cleanAll() {
		Log.e("NotificationService","cleanShare");
		if(share == null || share.edit() == null){
			return;
		}
		SharedPreferences.Editor editor = share.edit();
		if(editor != null){
			editor.clear();
			editor.commit();
		}
		
	}

	@Override
	public void onCreate() {
		// TODO Auto-generated method stub
		super.onCreate();
//		Log.e("seveice","---onCreate");
		share = getSharedPreferences(PUSH_TAG, MODE_PRIVATE);
		
		if(getPushInfo(PUSH_KEY+PUSH_ID).equals("")){
			pushId = 0;
		}else{
			pushId = Integer.valueOf(getPushInfo(PUSH_KEY+PUSH_ID));
		}
		new Thread(new MyThread()).start();
	}

	public static void showNotification(String paramJson) {
		
		try {
			JSONObject json = new JSONObject(paramJson);
			//if(pushId==0){
				pushId++;
				savePushInfo(PUSH_ID, String.valueOf(pushId));
				savePushInfo(String.valueOf(pushId),json.getString("date") + "&" + json.getString("news") + "&"+json.getString("title") +"&" +json.getString("repeatType"));
//			}else{
//				String info[] = getPushInfo(PUSH_KEY + pushId).split("&");
//				if(!json.getString("news").equals(info[1])){
//					pushId++;
//					savePushInfo(PUSH_ID, String.valueOf(pushId));
//					savePushInfo(String.valueOf(pushId),json.getString("date") + "&" + json.getString("news") + "&"+ json.getString("repeatType"));
//				}
//			}
			

		} catch (JSONException e) {
			e.printStackTrace();
		}
	}

	public class MyThread implements Runnable { // thread
		@Override
		public void run() {
			while (true) {
				try {
					Thread.sleep(30000); // sleep 1000ms
//					Log.e("---Runnable ", "--pushId = "+pushId);
					if (pushId != 0) {
						for (int i = 1; i <= pushId; i++) {
							String info[] = getPushInfo(PUSH_KEY + i).split("&");
							if (info.length == 4) {
								int pushDate = Integer.valueOf(info[0]);
								Log.e("---pushDate ", "--pushDate = "+pushDate);
								//int pushDate = 7271800;
								int realDate = Integer.valueOf(getStringDate());
								Log.e("---realDate ", "--realDate = "+realDate);
						    	
						    	int pushMonth = pushDate/1000000;
						    	int pushDay = pushDate / 10000 % 100;
						    	
						    	int realMonth = realDate/1000000;
						    	int realDay = realDate / 10000 % 100;
						    	
						    	String todayDate;
						    	
					    		if(realDate>pushDate){
					    			todayDate = addTime(info[0], realDay-pushDay,realMonth-pushMonth);	
					    			if(pushMonth==1&&realMonth==12){
					    				todayDate = info[0];
					    			}
					    		}else{
					    			todayDate = info[0];
					    		}
						    	
						    	//Log.e("---todayDate ", "--todayDate = "+todayDate);
						    	 
								int range = Integer.valueOf(todayDate) -Integer.valueOf(realDate);
								//Log.e("---range ", "--range = "+range);
								if (-1<=range&&range<=1) {
									int repeatType = Integer.valueOf(info[3]);
									if (repeatType == 0) {// 不重复
//										Log.e("--show----", "--------show0");
										show(info[1],info[2]);
										deleteInfo(PUSH_KEY + i);
									} else if (repeatType == 1) {// 日重复
//										Log.e("--show----", "--------show1");
										show(info[1],info[2]);
										savePushInfo(String.valueOf(i),addTime(todayDate, 1,0) + "&"+ info[1] + "&"+ info[2] +"&"+ repeatType);
									} else if (repeatType == 2) {// 周重复
//										Log.e("--show----", "--------show2");
										show(info[1],info[2]);
										savePushInfo(String.valueOf(i),addTime(todayDate, 7,0) + "&"+ info[1] + "&"+ info[2] + "&"+ repeatType);
									}
								}
							}

						}
					}
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
	}

	@Override
	public IBinder onBind(Intent intent) {
		// TODO Auto-generated method stub
		return null;
	}

	public void show(String info,String title) {
		tag++;
		String ns = Context.NOTIFICATION_SERVICE;
		NotificationManager nm = (NotificationManager) getSystemService(ns);

		long when = System.currentTimeMillis();

//		Notification notification = new Notification(R.drawable.app_icon,info, when);
//		notification.flags = Notification.FLAG_AUTO_CANCEL;
		PendingIntent pi = PendingIntent.getActivity(this, 0, new Intent(this,
				MainActivity.class), 0);
//		notification.setLatestEventInfo(this, title, info, pi);
//		nm.notify(tag, notification);
		
		
		int resID = getResources().getIdentifier("android_192", "drawable", this.getPackageName());
		NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this);  
		mBuilder.setContentTitle(title)
				.setContentText(info)
				.setContentIntent(pi) 
				.setWhen(when)
//				.setPriority(Notification.PRIORITY_DEFAULT) 
//				.setDefaults(Notification.FLAG_SHOW_LIGHTS)
				.setDefaults(Notification.DEFAULT_ALL) 
				.setSmallIcon(resID);
		
		
		Notification notification = mBuilder.build();  
		notification.flags = Notification.FLAG_AUTO_CANCEL; 
		nm.notify(tag, notification);
	}

	public static String getStringDate() {
			Date currentTime = new Date();
			SimpleDateFormat formatter = new SimpleDateFormat("MMddHHmm");
			String dateString = formatter.format(currentTime);
//			Log.e("Notifity", "--------getStringDate------dateString = " + dateString);
			return getPrettyNumber(dateString);
	}
	 public String addTime(String date,int day,int month){
		 	DateFormat format;
		 	 Date temp_date = null ;
		 	if(date.length()==8){
		 		format = new SimpleDateFormat("MMddHHmm");
		 	}else{
		 		format = new SimpleDateFormat("MddHHmm");  
		 	}
	        try {  
	            Date d = format.parse(date);  
	            Calendar c = Calendar.getInstance();  
	            c.setTime(d);  
	            c.add(c.DATE, day);  
	            c.add(c.MONTH, month);  
	            temp_date = c.getTime();   
	        } catch (ParseException e) {  
	            e.printStackTrace();  
	        }
			return getPrettyNumber(format.format(temp_date)); 
	 }
	 public static String getPrettyNumber(String number) {  
		 if(number!=null){
			 return BigDecimal.valueOf(Double.parseDouble(number))  
			            .stripTrailingZeros().toPlainString();  
		 }else{
			 return null; 
		 }
	}
	 @Override
	public void onDestroy() {
		// TODO Auto-generated method stub
//		 Log.i("---", "ExampleService-onDestroy:::");  
		 onStart(mintent,mstartId);
	}
	@Override
	public void onStart(Intent intent, int startId) {
		// TODO Auto-generated method stub
		super.onStart(intent, startId);
		Log.d("lwn", "NotificationService Start--------------------------");
	}
	Intent mintent;
	int mstartId;
	@Override  
	    public int onStartCommand(Intent intent, int flags, int startId) {  
			//执行文件的下载或者播放等操作  
//	        Log.i("---", "ExampleService-onStartCommand:::"+startId);  
	        mintent = intent;
	        mstartId = startId;
	        /*  
	         * 这里返回状态有三个值，分别是:  
	         * 1、START_STICKY：当服务进程在运行时被杀死，系统将会把它置为started状态，但是不保存其传递的Intent对象，之后，系统会尝试重新创建服务;  
	         * 2、START_NOT_STICKY：当服务进程在运行时被杀死，并且没有新的Intent对象传递过来的话，系统将会把它置为started状态，  
	         *   但是系统不会重新创建服务，直到startService(Intent intent)方法再次被调用;  
	         * 3、START_REDELIVER_INTENT：当服务进程在运行时被杀死，它将会在隔一段时间后自动创建，并且最后一个传递的Intent对象将会再次传递过来。  
	         */   
	        return super.onStartCommand(intent, flags, startId);  
	    } 
}
