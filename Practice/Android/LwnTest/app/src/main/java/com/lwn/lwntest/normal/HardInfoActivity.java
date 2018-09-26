package com.lwn.lwntest.normal;

import android.Manifest;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.content.Context;
import android.content.pm.PackageManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.telephony.TelephonyManager;
import android.util.DisplayMetrics;
import android.view.Display;
import android.widget.TextView;

import com.lwn.lwntest.R;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

/**
 * Created by liweina on 2018/9/19.
 */

public class HardInfoActivity extends Activity {
    private TelephonyManager phone;
    private WifiManager wifi;
    private Display display;
    private DisplayMetrics metrics;

    public final int REQUEST_READ_PHONE_STATE = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_hardinfo);

        phone = (TelephonyManager) this.getApplicationContext().getSystemService(Context.TELEPHONY_SERVICE);
        wifi = (WifiManager) this.getApplicationContext().getSystemService(Context.WIFI_SERVICE);
        display = this.getWindowManager().getDefaultDisplay();

        metrics = this.getResources().getDisplayMetrics();

        int permissionCheck = ContextCompat.checkSelfPermission(this, Manifest.permission.READ_PHONE_STATE);
        if (permissionCheck != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[] {Manifest.permission.READ_PHONE_STATE},REQUEST_READ_PHONE_STATE);
        }

    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {
            case REQUEST_READ_PHONE_STATE:
                if ((grantResults.length > 0) && (grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                    //TODO
                    init();
                }
                break;
            default:
                break;
        }
    }

    private void init() {
        DisplayMetrics book=new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(book);




        try {
            Class localClass = Class.forName("android.os.SystemProperties");
            Object localObject1 = localClass.newInstance();
            Object localObject2 = localClass.getMethod("get", new Class[] { String.class, String.class }).invoke(localObject1, new Object[] { "gsm.version.baseband", "no message" });
            Object localObject3 = localClass.getMethod("get", new Class[] { String.class, String.class }).invoke(localObject1, new Object[] { "ro.build.display.id",""});


            setEditText(R.id.get,localObject2+"");

            setEditText(R.id.osVersion,localObject3+"");
        } catch (Exception e) {
            e.printStackTrace();
        }



        //获取网络连接管理者
        ConnectivityManager connectionManager = (ConnectivityManager)
                getSystemService(CONNECTIVITY_SERVICE);
        //获取网络的状态信息，有下面三种方式
        NetworkInfo networkInfo = connectionManager.getActiveNetworkInfo();

        if(networkInfo != null){
            setEditText(R.id.lianwang,networkInfo.getType()+"");
            setEditText(R.id.lianwangname,networkInfo.getTypeName());
        }

        setEditText(R.id.imei, phone.getDeviceId());
        setEditText(R.id.deviceversion,phone.getDeviceSoftwareVersion());
        setEditText(R.id.imsi, phone.getSubscriberId());
        setEditText(R.id.number, phone.getLine1Number());
        setEditText(R.id.simserial, phone.getSimSerialNumber());
        setEditText(R.id.simoperator,phone.getSimOperator());
        setEditText(R.id.simoperatorname, phone.getSimOperatorName());
        setEditText(R.id.simcountryiso, phone.getSimCountryIso());
        setEditText(R.id.workType,phone.getNetworkType()+"");
        setEditText(R.id.netcountryiso,phone.getNetworkCountryIso());
        setEditText(R.id.netoperator,phone.getNetworkOperator());
        setEditText(R.id.netoperatorname,phone.getNetworkOperatorName());


        setEditText(R.id.radiovis,android.os.Build.getRadioVersion());
        setEditText(R.id.wifimac, wifi.getConnectionInfo().getMacAddress());
        setEditText(R.id.getssid,wifi.getConnectionInfo().getSSID());
        setEditText(R.id.getbssid,wifi.getConnectionInfo().getBSSID());
        setEditText(R.id.ip,wifi.getConnectionInfo().getIpAddress()+"");
        setEditText(R.id.bluemac, BluetoothAdapter.getDefaultAdapter()
                .getAddress());
        setEditText(R.id.bluname, BluetoothAdapter.getDefaultAdapter().getName()
        );

        setEditText(R.id.cpu,getCpuName());


        setEditText(R.id.andrlid_id,
                Settings.Secure.getString(getContentResolver(), Settings.Secure.ANDROID_ID));
        setEditText(R.id.serial,android.os.Build.SERIAL);
        setEditText(R.id.brand,android.os.Build.BRAND);
        setEditText(R.id.tags, android.os.Build.TAGS);
        setEditText(R.id.device,android.os.Build.DEVICE);
        setEditText(R.id.fingerprint,android.os.Build.FINGERPRINT);
        setEditText(R.id.bootloader, Build.BOOTLOADER);
        setEditText(R.id.release, Build.VERSION.RELEASE);
        setEditText(R.id.sdk,Build.VERSION.SDK);
        setEditText(R.id.sdk_INT,Build.VERSION.SDK_INT+"");
        setEditText(R.id.codename,Build.VERSION.CODENAME);
        setEditText(R.id.incremental,Build.VERSION.INCREMENTAL);
        setEditText(R.id.cpuabi, android.os.Build.CPU_ABI);
        setEditText(R.id.cpuabi2, android.os.Build.CPU_ABI2);
        setEditText(R.id.board, android.os.Build.BOARD);
        setEditText(R.id.model, android.os.Build.MODEL);
        setEditText(R.id.product, android.os.Build.PRODUCT);
        setEditText(R.id.type, android.os.Build.TYPE);
        setEditText(R.id.user, android.os.Build.USER);
        setEditText(R.id.disply, android.os.Build.DISPLAY);
        setEditText(R.id.hardware, android.os.Build.HARDWARE);
        setEditText(R.id.host, android.os.Build.HOST);
        setEditText(R.id.changshang, android.os.Build.MANUFACTURER);
        setEditText(R.id.phonetype,phone.getPhoneType()+"");
        setEditText(R.id.simstate,phone.getSimState()+"");
        setEditText(R.id.b_id, Build.ID);
        setEditText(R.id.gjtime,android.os.Build.TIME+"");
        setEditText(R.id.width,display.getWidth()+"");
        setEditText(R.id.height,display.getHeight()+"");
        setEditText(R.id.dpi,book.densityDpi+"");
        setEditText(R.id.density,book.density+"");
        setEditText(R.id.xdpi,book.xdpi+"");
        setEditText(R.id.ydpi,book.ydpi+"");
        setEditText(R.id.scaledDensity,book.scaledDensity+"");



        //setEditText(R.id.wl,getNetworkState(this)+"");
        // 方法2
        DisplayMetrics dm = new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(dm);
        int width=dm.widthPixels;
        int  height=dm.heightPixels;

        setEditText(R.id.xwidth,width+"");
        setEditText(R.id.xheight,height+"");

    }

    private void setEditText(int id, String s) {
        ((TextView) this.findViewById(id)).setText(s);
    }
    /**
     * 获取CPU型号
     * @return
     */
    public static String getCpuName() {

        String str1 = "/proc/cpuinfo";
        String str2 = "";

        try {
            FileReader fr = new FileReader(str1);
            BufferedReader localBufferedReader = new BufferedReader(fr);
            while ((str2 = localBufferedReader.readLine()) != null) {
                if (str2.contains("Hardware")) {
                    return str2.split(":")[1];
                }
            }
            localBufferedReader.close();
        } catch (IOException e) {
        }
        return null;
    }


    }
