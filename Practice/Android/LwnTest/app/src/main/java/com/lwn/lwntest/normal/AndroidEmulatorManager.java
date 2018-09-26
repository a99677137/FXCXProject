package com.lwn.lwntest.normal;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


import android.bluetooth.BluetoothAdapter;
import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorManager;
import android.os.Build;
import android.text.TextUtils;
import android.util.Log;

public class AndroidEmulatorManager {

	public static boolean CheckIsEmulator(Context context){
		boolean result = false;
		try{
			result= HasNoBlueTooth() 
					|| notHasLightSensorManager(context) 
					|| DeviceInfoIsEmulator()
					|| CheckCpuEmulator();
			Log.e("LWN","---------------------AndroidEmulatorManager:HasNoBlueTooth = " +HasNoBlueTooth());
			Log.e("LWN","---------------------AndroidEmulatorManager:notHasLightSensorManager(context) = " +notHasLightSensorManager(context));
			Log.e("LWN","---------------------AndroidEmulatorManager:DeviceInfoIsEmulator() = " +DeviceInfoIsEmulator());
			Log.e("LWN","---------------------AndroidEmulatorManager:CheckCpuEmulator()= " +CheckCpuEmulator());
			Log.e("LWN","---------------------AndroidEmulatorManager:IsEmulatorManager-------result="+result );
		}
		catch(Exception ex){
			Log.e("LWN","---------------------AndroidEmulatorManager:IsEmulatorManager----Exception = " + ex.toString());
		}
		return result;
	}
	
	public static boolean HasNoBlueTooth(){
		BluetoothAdapter adapter = BluetoothAdapter.getDefaultAdapter();
		if(adapter == null){
			Log.e("LWN","---------------------AndroidEmulatorManager:HasNoBlueTooth------adapter = null" );
			return true;
		}else{
			//如果有蓝牙不一定是有效的。获取蓝牙名称，若为null 则默认为模拟器
			String name = adapter.getName();
			if(TextUtils.isEmpty(name)){
				Log.e("LWN","---------------------AndroidEmulatorManager:HasNoBlueTooth------adapter.getName() = null" );
				return true;
			}else{
				return false;
			}
		}
		
	}
	
	/**
	 * 判断是否存在光传感器来判断是否为模拟器
	 * 部分真机也不存在温度和压力传感器。其余传感器模拟器也存在。
	 * @return true 为模拟器
	 */
	public static Boolean notHasLightSensorManager(Context context) {
	    SensorManager sensorManager = (SensorManager) context.getSystemService(Context.SENSOR_SERVICE);
	    Sensor sensor8 = sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT); //光
	    if (null == sensor8) {
	        return true;
	    } else {
	        return false;
	    }
	}
	
	public static boolean DeviceInfoIsEmulator(){
		return Build.FINGERPRINT.startsWith("generic") 
			|| Build.FINGERPRINT.toLowerCase().contains("vbox")
			|| Build.FINGERPRINT.toLowerCase().contains("test-keys")
			|| Build.MODEL.contains("google_sdk")
			|| Build.MODEL.contains("Emulator")
			|| Build.MODEL.contains("Android SDK built for x86")
            || Build.MANUFACTURER.contains("Genymotion")
            || (Build.BRAND.startsWith("generic") && Build.DEVICE.startsWith("generic"))
            || "google_sdk".equals(Build.PRODUCT);
		
	}
	
	public static String ReadCpuInfo(){
		String result = "";
		try {
	        String[] args = {"/system/bin/cat", "/proc/cpuinfo"};
	        ProcessBuilder cmd = new ProcessBuilder(args);

	        Process process = cmd.start();
	        StringBuffer sb = new StringBuffer();
	        String readLine = "";
	        BufferedReader responseReader = new BufferedReader(new InputStreamReader(process.getInputStream(), "utf-8"));
	        while ((readLine = responseReader.readLine()) != null) {
	            sb.append(readLine);
	        }
	        responseReader.close();
	        result = sb.toString().toLowerCase();
			Log.e("LWN","---------------------AndroidEmulatorManager:ReadCpuInfo------result = " + result );
	    } catch (IOException ex) {
	    	Log.e("LWN","---------------------AndroidEmulatorManager:ReadCpuInfo----IOException = " + ex.toString());
	    }
	    return result;
	}
	
	/**
	 * 判断cpu是否为电脑来判断 模拟器
	 * @return true 为模拟器
	 */
	public static boolean CheckCpuEmulator() {
	    String cpuInfo = ReadCpuInfo();
	    if ((cpuInfo.contains("intel") || cpuInfo.contains("amd"))) {
	    	Log.e("LWN","---------------------AndroidEmulatorManager:CheckCpuEmulator-------------reutrn true");
	        return true;
	    }
	    return false;
	}

}
