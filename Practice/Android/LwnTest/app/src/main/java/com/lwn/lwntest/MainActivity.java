package com.lwn.lwntest;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.ComponentName;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Process;
import android.provider.Settings;
import android.support.v4.content.FileProvider;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.lwn.lwntest.flatbuffers.FlatbuffersActivity;
import com.lwn.lwntest.normal.AndroidEmulatorManager;
import com.lwn.lwntest.normal.HardInfoActivity;
import com.lwn.lwntest.normal.LogCatActivity;
import com.lwn.lwntest.normal.LwnTestUtil;
import com.lwn.lwntest.normal.NotificationService;
import com.lwn.lwntest.normal.PropertyUtils;
import com.lwn.lwntest.normal.ScreenShotHelper;
import com.lwn.lwntest.normal.TlbbLog;

import java.io.File;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    // Used to load the 'native-lib' library on application startup.
    static {
        System.loadLibrary("native-lib");
    }

    public static MainActivity CurActivity = null;

    private static int CurAndroidApiLevel = 24;
    private static String ObserverGameObject = "Bridge";

    private static String LwnTag = "LiWeinaTest";

    private static String msg = "OnCreate";

    private static TextView msgTextView ;

    ComponentName newIconComponentName = null;
    ComponentName defaultComponentName = null;

    PackageManager packageManager = null;

    public static int ProcessId = -1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ProcessId = android.os.Process.myPid();

        TlbbLog.d("Process Id = " + ProcessId);

        // Example of a call to a native method
        TextView tv = (TextView) findViewById(R.id.sample_text);
        tv.setText(stringFromJNI());



        CurActivity = this;
        TlbbLog.d("----------onCreate---------------");
        //ShowSysDialog("Test","LWNTest!!!","OK","Cancel");

        StartNotificationServices();//启动推送services

        TextView statuTxt =  (TextView)this.findViewById(R.id.prop);

        //-----------------------------------------------动态换Icon begin--------------------------------------------------------------
        packageManager = this.getApplicationContext().getPackageManager();

        TlbbLog.d("----------------OnCreate----Current packeagename = " + this.getPackageName());
        String packageName = this.getPackageName();

        defaultComponentName = this.getComponentName();
        newIconComponentName = new ComponentName(this.getBaseContext(),packageName+".ChangeIcon");

        //disableComponent(newIconComponentName);


        Button changeIcon = (Button)this.findViewById(R.id.ChangeIcon);
        changeIcon.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                disableComponent(defaultComponentName);
                enableComponent(newIconComponentName);
            }
        });
        //-----------------------------------------------动态换Icon end-----------------------------------------------------------

        Button callLogcat = (Button)this.findViewById(R.id.CallLogcat);
        callLogcat.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intenta = new Intent();
                intenta.setClass(MainActivity.this, LogCatActivity.class);
                MainActivity.this.startActivity(intenta);
            }
        });


        //-------------------手游助手 获取VT接口------------------------------------------------------------------------
        String status = PropertyUtils.get("androVM.vt_status", "Lwn");//by weina 这个系统参数是在腾讯手游助手上面特有的~但是获取系统配置的接口是好用的~~
        TlbbLog.d( "----------onCreate---------------status = " + status);
        statuTxt.setText("Property:"+status);

        //-------------------手游助手 获取VT接口 end------------------------------------------------------------------------

        TextView IMEITxt =  (TextView)this.findViewById(R.id.IMEI);
        String SpeicalImei = PropertyUtils.get("android.device.id", "Lwn");
        TlbbLog.d("----------onCreate---------------SpeicalImei = " + SpeicalImei);
        IMEITxt.setText("PropertyIMEI:"+SpeicalImei);


        TextView helloworld = (TextView)this.findViewById(R.id.HelloWorld);
        String res = LwnTestUtil.randomString(-229985452)+' '+ LwnTestUtil.randomString(-147909649);
        helloworld.setText(res);




        ScreenShotHelper.GetInstance().OnCreate4CutScreen();

        TlbbLog.d("----------onCreate---------------CheckIsEmulator");
        boolean IsEmulator = AndroidEmulatorManager.CheckIsEmulator(CurActivity);
        TlbbLog.d("----------onCreate---------------IsEmulator = "+ IsEmulator);
        TextView emulator = (TextView) this.findViewById(R.id.Emulator);
        String emulatorStr = "IsEmulator:" + IsEmulator;
        emulator.setText(emulatorStr);


        TextView emulatorDetail = (TextView) this.findViewById(R.id.EmulatorDetail);
        String emulatorDetailStr = "HasNoBlueTooth:" + AndroidEmulatorManager.HasNoBlueTooth() +
                "! notHasLightSensorManager:"+AndroidEmulatorManager.notHasLightSensorManager(this)+
                "! DeviceInfoIsEmulator:"+AndroidEmulatorManager.DeviceInfoIsEmulator() +
                "! CheckCpuEmulator:" + AndroidEmulatorManager.CheckCpuEmulator();
        emulatorDetail.setText(emulatorDetailStr);

        //------------------------------------------获取IMEI接口---------------------------------------------------------------
        //by weina 在Android6。0以上得系统都要动态申请权限
//        TextView IMEITxt =  (TextView)this.findViewById(R.id.IMEI);
//        TelephonyManager tm = (TelephonyManager) this.getSystemService(TELEPHONY_SERVICE);
//        IMEITxt.setText("IMEI:"+tm.getDeviceId());

        //------------------------------------------获取IMEI接口 end---------------------------------------------------------------


        //---------------------------Uri-------------------------------------------
        Button callDial = (Button) this.findViewById(R.id.CallDial);
        callDial.setOnClickListener(new View.OnClickListener(){

            @Override
            public void onClick(View arg0) {
                // TODO Auto-generated method stub
                Uri callUri = Uri.parse("tel:15011518884");
                Intent callIntent = new Intent(Intent.ACTION_DIAL,callUri);
                startActivity(callIntent);
            }

        });

        Button callLocation = (Button)this.findViewById(R.id.CallLocation);
        callLocation.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Uri callUri = Uri.parse("geo:0,0?q=1600+Amphitheatre+Parkway,+Mountain+View,+California");
                Intent intent = new Intent(Intent.ACTION_VIEW,callUri);
                if(CheckIntentIsExist(intent)){
                    startActivity(intent);
                }else{
                    Toast.makeText(CurActivity, "Cant find LocationApp!!", Toast.LENGTH_LONG).show();
                }
            }
        });

        Button callBrowse = (Button)this.findViewById(R.id.CallWebBrowse);
        callBrowse.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Uri callUri = Uri.parse("http://WWW.baidu.com");
                Intent intent = new Intent(Intent.ACTION_VIEW,callUri);
                if(CheckIntentIsExist(intent)){
                    startActivity(intent);
                }else{
                    Toast.makeText(CurActivity, "Cant find WebBrowse!!",Toast.LENGTH_LONG).show();
                }
            }
        });

        Button callQQBtn = (Button)this.findViewById(R.id.CallQQ);
        callQQBtn.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Uri callQQUri = Uri.parse("mqqapi://forward/url?src_type=web&version=1&url_prefix=aHR0cHM6Ly9wYXkucXEuY29tLw==");
                Intent callQQ = new Intent(Intent.ACTION_VIEW,callQQUri);
                if(CheckIntentIsExist(callQQ)){
                    startActivity(callQQ);
                }else{
                    Toast.makeText(CurActivity, "Cant find QQ!!",Toast.LENGTH_LONG).show();
                }
            }
        });

        Button callActivityBtn = (Button)this.findViewById(R.id.CallActivity);
        callActivityBtn.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Uri callAct = Uri.parse("lwntestactivity://aa.bb/test?Id=1&Name=lwn");
                Intent callActivity = new Intent(Intent.ACTION_VIEW,callAct);
                if(CheckIntentIsExist(callActivity)){
                    startActivity(callActivity);
                }else{
                    Toast.makeText(CurActivity, "Cant find Activity!!",Toast.LENGTH_LONG).show();
                }
            }
        });

        //---------------------------Uri end-------------------------------------------

        //----------------------------flatbufferrs---------------------------------------
        Button CallFlatbuffers = (Button)this.findViewById(R.id.CallFlatbuffers);
        CallFlatbuffers.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Intent intenta = new Intent();
                intenta.setClass(MainActivity.this, FlatbuffersActivity.class);
                MainActivity.this.startActivity(intenta);
            }
        });
        //----------------------------flatbufferrs end---------------------------------------


        //----------------------------Device HardInfo begin----------------------------------------
        Button CallHardInfo = (Button)this.findViewById(R.id.CallHardInfo);
        CallHardInfo.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                Intent intenta = new Intent();
                intenta.setClass(MainActivity.this, HardInfoActivity.class);
                MainActivity.this.startActivity(intenta);
            }
        });

        //----------------------------Device HardInfo end----------------------------------------




        //----------------------网页拉起游戏-----------------------------------
        //scheme://host:port/path？query#fragment
        //网页内容:<a href="lwntest://aa.bb:80/test?p=12&d=1">test</a>
        //网页和manifest中的sheme参数要小写字母
        Intent intent = getIntent();
        String scheme = intent.getScheme();
        Uri uri = intent.getData();
        TlbbLog.d("scheme:"+scheme);  //scheme:lwntest
        if (uri != null) {
            String host = uri.getHost();
            String dataString = intent.getDataString();
            String id = uri.getQueryParameter("d");
            String path = uri.getPath();
            String path1 = uri.getEncodedPath();
            String queryString = uri.getQuery();
            TlbbLog.d("host:"+host);  //host:aa.bb
            TlbbLog.d("dataString:"+dataString);  //dataString:lwntest://aa.bb:80/test?p=12&d=1
            TlbbLog.d("d:"+id);  //d:1
            TlbbLog.d("path:"+path);  //path:/test
            TlbbLog.d("path1:"+path1);  //path1:/test
            TlbbLog.d("queryString:"+queryString);  //queryString:p=12&d=1
        }

        //----------------------网页拉起游戏-----------------------------------

        //----------------省电量UI-------------------------------------------
        Button lowBtn = (Button)this.findViewById(R.id.LowBrightBtn);
        Button highBtn = (Button) this.findViewById(R.id.HighBrightBtn);

        msgTextView = (TextView)this.findViewById(R.id.msg);
        msgTextView.setText((CharSequence) msg);
        lowBtn.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                //SetSystemBrightness(10);
//				SetLowBrightness(10);
                SetBrightness(10);
            }
        });
        highBtn.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                //SetSystemBrightness(-1);
//				SetHighBrightness();
                SetBrightness(-1);
            }
        });
        //----------------省电量UI-------------------------------------------
        /**注册监听系统亮度改变事件*/
//        this.getContentResolver().registerContentObserver(Settings.System.getUriFor(Settings.System.SCREEN_BRIGHTNESS),true, BrightnessMode);



    }


    //-------------------------动态换icon begin---------------------------------------------------

    private void enableComponent(ComponentName componentName) {
        if(packageManager == null || componentName == null){
            return;
        }
        packageManager.setComponentEnabledSetting(componentName,
                PackageManager.COMPONENT_ENABLED_STATE_ENABLED,
                PackageManager.DONT_KILL_APP);
    }

    private void disableComponent(ComponentName componentName) {
        if(packageManager == null || componentName == null){
            return;
        }
        packageManager.setComponentEnabledSetting(componentName,
                PackageManager.COMPONENT_ENABLED_STATE_DISABLED,
                PackageManager.DONT_KILL_APP);
    }
//-------------------------动态换icon end---------------------------------------------------



    public boolean CheckIntentIsExist(Intent intent){
        PackageManager pm = getPackageManager();
        List activity = pm.queryIntentActivities(intent, PackageManager.MATCH_DEFAULT_ONLY);
        return activity.size()>0;
    }


    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        SetBrightness(-1);
        if (hasFocus) {
            TlbbLog.d( "onWindowFocusChanged:" + "true");
        } else {
            TlbbLog.d("onWindowFocusChanged:" + "false");
        }
    }

    @Override
    protected void onResume(){
        super.onResume();
        TlbbLog.d("----------onResume---------------");
        SetBrightness(-1);
    }


    @Override
    protected void onPause(){
        super.onPause();
        TlbbLog.d("----------onPause---------------");
        SetBrightness(-1);
    }


    //----------------------------打开系统提示框----------------------------------------
    public static void ShowSysDialog(String titile,String msg,String sureBtn,String cancelBtn ){
        TlbbLog.d("----------ShowSysDialog---------------");
        final String _title =titile;
        final String _msg =msg;
        final String _sureBtn = sureBtn;
        final String _cancelBtn = cancelBtn;
        CurActivity.runOnUiThread(new Runnable()
        {
            @Override
            public void run() {

                AlertDialog.Builder dialog = new AlertDialog.Builder(CurActivity);
                dialog.setTitle(_title) ;
                dialog.setMessage(_msg);
                dialog.setPositiveButton(_sureBtn, new DialogInterface.OnClickListener(){

                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        TlbbLog.d( "----------ShowSysDialog---------------onClick-OK");
                        //UnityPlayer.UnitySendMessage(ObserverGameObject, "OnSystemDialogClick","1");
                    }

                })  ;
                dialog.setNegativeButton(_cancelBtn, new DialogInterface.OnClickListener(){

                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        TlbbLog.d( "----------ShowSysDialog---------------onClick-Cancel");
                        //UnityPlayer.UnitySendMessage(ObserverGameObject, "OnSystemDialogClick","0");
                    }
                });
                dialog.setCancelable(false);
                dialog.create().show();
                TlbbLog.d("----------ShowSysDialog---------------!!!!!");
            }
        });



    }
    //----------------------------打开系统提示框 end----------------------------------------


    //-----------------------------暗屏幕 System-------------------------------------

    /**
     * by weina:直接修改系统亮度-没有采用
     * 原因:由于在安卓6以后修改系统设置需要动态申请权限~
     * 但是在6.0系统的手机上面有一个6.0系统bug~
     * 动态申请的时候,会打开一个设置UI,让玩家自己手动打开设置.但是这个设置UI在打开的时候默认是开启的!!!
     * 如果玩家看到默认的设置是开启的,就会点返回按钮了,这个时候就想定于没有申请到权限!
     * 必须手动关闭后再打开一遍的话这个权限就相当于没有拿到!简直坑爹....
     * 但是在7.0的系统上面修复了
     * 所以~实在不能抛弃6.0系统的手机
     */
    private static int normalBrightness = 0;
    private static int REQUEST_CODE_WRITE_SETTINGS = 66666;
    private static int brightTmp = 0;
    private static boolean isLow = false;
    @TargetApi(23)
    public static void SetSystemBrightness(int bright){
        TlbbLog.d("------------SetSystemBrightness:bright = " + bright);
        if(bright <= 0){
            bright = normalBrightness;
            isLow = false;
            TlbbLog.d("------------SetSystemBrightness:normalBrightness = " + normalBrightness +"! isLow = " + isLow);
        }
        else{
            if(!isLow){
                isLow = true;
                normalBrightness = GetCurSysBrightness();
                TlbbLog.d("----!isLow--------SetSystemBrightness:normalBrightness = " + normalBrightness +"! isLow = " + isLow);
            }
        }
        if(android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.M){
            //if (PackageManager.PERMISSION_GRANTED == ContextCompat.checkSelfPermission(CurActivity, Manifest.permission.WRITE_SETTINGS)) {
            //has permission, do operation directly
            //	RealSetSysBrightness(bright);
            //    TlbbLog.d("user has the permission already!");
            //} else {
//				ActivityCompat.requestPermissions(MainActivity.this,
//		                new String[]{Manifest.permission.WRITE_SETTINGS},
//		                MY_PERMISSIONS_REQUEST_READ_CONTACTS);
            //TlbbLog.d("user does't have the permission!");
            //if (ActivityCompat.shouldShowRequestPermissionRationale(CurActivity,
            //Manifest.permission.WRITE_SETTINGS)) {

            // Show an explanation to the user *asynchronously* -- don't block
            // this thread waiting for the user's response! After the user
            // sees the explanation, try again to request the permission.
            //   TlbbLog.d( "we should explain why we need this permission!");
            //} else {
            // No explanation needed, we can request the permission.
            TlbbLog.d( "==request the permission==");
            if(!Settings.System.canWrite(CurActivity) ){
                brightTmp = bright;
                Intent intent = new Intent(Settings.ACTION_MANAGE_WRITE_SETTINGS,Uri.parse("package:" + CurActivity.getPackageName()));
                intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                CurActivity.startActivityForResult(intent, REQUEST_CODE_WRITE_SETTINGS);
                TlbbLog.d( "user does't have the permission!");
            }else if(Settings.System.canWrite(CurActivity)){
                RealSetSysBrightness(bright);
                TlbbLog.d( "user has the permission already!");
            }
            //}
            //}

        }else{
            RealSetSysBrightness(bright);
        }
    }

    @TargetApi(23)
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == REQUEST_CODE_WRITE_SETTINGS) {
            if (Settings.System.canWrite(this)) {
                TlbbLog.d("onActivityResult write settings granted" );
                if(brightTmp >0){
                    RealSetSysBrightness(brightTmp);
                }
            }
        }
    }
    public static int GetCurSysBrightness() {
        int curBrightness = 0;
        try {
            curBrightness = Settings.System.getInt(CurActivity.getContentResolver(), Settings.System.SCREEN_BRIGHTNESS);
        } catch (Settings.SettingNotFoundException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        TlbbLog.d("----------GetCurSysBrightness:curBrightness= " + curBrightness);
//        String result ="" + curBrightness;
//        TlbbLog.d( "----------getCurBrightness:result = " + result);
//        UnityPlayer.UnitySendMessage(ObserverGameObject, "OnGetCurBrightness",result);
        return curBrightness;
    }
    private static void RealSetSysBrightness(int brightness){
        final int brightnessfinal = brightness;
        try{
            CurActivity.runOnUiThread(new Runnable()
            {
                @Override
                public void run() {
                    float val = Float.valueOf(GameSystemBrightness) * (1f/255f);
                    /**
                     WindowManager.LayoutParams lp = CurActivity.getWindow().getAttributes();
                     TlbbLog.d( "------val = " + val +"  GameSystemBrightness = " + GameSystemBrightness);
                     lp.screenBrightness = val;
                     CurActivity.getWindow().setAttributes(lp);
                     */
                    TlbbLog.d("----------RealSetSysBrightness:brightnessfinal = " + brightnessfinal);
                    Uri uri = Settings.System.getUriFor(Settings.System.SCREEN_BRIGHTNESS);
                    Settings.System.putInt(CurActivity.getContentResolver(), Settings.System.SCREEN_BRIGHTNESS, brightnessfinal);
                    CurActivity.getContentResolver().notifyChange(uri, null);
                }
            });
        }catch(Exception e){
            e.printStackTrace();
        }

    }
    // 停止自动亮度调节
    public static void stopAutoBrightness(Activity activity) {

        Settings.System.putInt(activity.getContentResolver(),
                Settings.System.SCREEN_BRIGHTNESS_MODE,
                Settings.System.SCREEN_BRIGHTNESS_MODE_MANUAL);
    }
    // 开启自动亮度调节
    public static void startAutoBrightness(Activity activity) {

        Settings.System.putInt(activity.getContentResolver(),
                Settings.System.SCREEN_BRIGHTNESS_MODE,
                Settings.System.SCREEN_BRIGHTNESS_MODE_AUTOMATIC);
    }
    //------------------------------暗屏幕 System End---------------------------------

    //------------------------------------------------暗屏幕 CurActivity-----------------------------------------------------
    public static int GameSystemBrightness = 0;

    public static int ActivityBrightness = 0;

    private static boolean hasLow = false;

    /**
     * by weina 设置观察者监听系统亮度的改变~也没有使用~
     * 原因:由于修改当前Window的亮度有一个特殊值,就是是否使用系统亮度
     * 一旦修改了一次Window的亮度后,在手机上面滑下来设置菜单,修改手机系统亮度的时候,根本修改不成功.所以都收不到观察者的事件....
     * 所以下面的demo修改了~就是在Window失去焦点后~立即就将当前Window设置为WindowManager.LayoutParams.BRIGHTNESS_OVERRIDE_NONE;就会立即同步为系统的亮度~
     * 同步了系统的亮度~就可以在下滑菜单中愉快的修改系统的亮度~回到游戏后会立即同步~
     * 恩恩~梦幻西游一定也是用的这种办法~因为他们也完全没有去动态申请修改系统设置的权限~
     * 纠结了我至少三四天才发现这个API~
     */
    /**
     //注册监听系统亮度改变事件
     //this.getContentResolver().registerContentObserver(Settings.System.getUriFor(Settings.System.SCREEN_BRIGHTNESS),true, BrightnessMode);
     //时刻监听系统亮度改变事件
     private ContentObserver BrightnessMode = new ContentObserver(new Handler()) {
    @Override
    public void onChange(boolean selfChange) {
    super.onChange(selfChange);
    //		    TlbbLog.d( "----------Brightness:onChange----selfChange = " + selfChange);
    //		    GameSystemBrightness = getSystemBrightness();
    //		    TlbbLog.d("----------Brightness:onChange----GameSystemBrightness = " + GameSystemBrightness);
    //		    RealSetActivityBrightness(GameSystemBrightness);
    if(CurActivity == null){
    msg = "OnChange:selfChange = " + selfChange +" CurActivity == null  Return";
    msgTextView.setText((CharSequence) msg);
    return;
    }

    msg = "OnChange:selfChange = " + selfChange;
    msgTextView.setText((CharSequence) msg);
    }
    };
     */

//    public static int getSystemBrightness() {
//    	int systembrightness = 0;
//	     try {
//	    	 systembrightness = Settings.System.getInt(CurActivity.getContentResolver(), Settings.System.SCREEN_BRIGHTNESS);
//	         TlbbLog.d( "----------getSystemBrightness:GameSystemBrightness = " + systembrightness);
//	     } catch (Settings.SettingNotFoundException e) {
//	         e.printStackTrace();
//	     }
//	     msg = "getSystemBrightness:systembrightness = " + systembrightness;
//	     msgTextView.setText((CharSequence) msg);
//	     return systembrightness;
//     }

//    public static int getCurActivityBrightness() {
//		int curBrightness = 0;
//		try {
//			curBrightness = Settings.System.getInt(CurActivity.getContentResolver(), Settings.System.SCREEN_BRIGHTNESS);
//		} catch (SettingNotFoundException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//		TlbbLog.d( "----------getCurBrightness:ActivityBrightness = " + curBrightness);
//		msg = "getCurActivityBrightness:curBrightness= " + curBrightness;
//	    msgTextView.setText((CharSequence) msg);
//
//		return curBrightness;
//     }

    public static void SetBrightness(int brightness){
        float light = -1;
        TlbbLog.d("-----------SetBrightness brightness = " +brightness);
        if(brightness == 0){
            brightness = 1;
            light = Float.valueOf(brightness) * (1f/255f);
        }else if(brightness >= 255){
            brightness = 255;
            light = Float.valueOf(brightness) * (1f/255f);
        }else if(brightness < 0){
            light = WindowManager.LayoutParams.BRIGHTNESS_OVERRIDE_NONE;
        }else{
            light = Float.valueOf(brightness) * (1f/255f);
            TlbbLog.d( "-----------SetBrightness brightness = " + brightness +"! light = "+light);
        }
        TlbbLog.d("-----------SetBrightness brightness = " + brightness +"! light = "+light);
        final float finallight = light;
        try{
            CurActivity.runOnUiThread(new Runnable()
            {
                @Override
                public void run() {
                    WindowManager.LayoutParams lp = CurActivity.getWindow().getAttributes();
                    lp.screenBrightness = finallight;
                    CurActivity.getWindow().setAttributes(lp);
                    TlbbLog.d( "-----------SetBrightness finish!!!!!");
                }
            });
        }catch(Exception e){
            e.printStackTrace();
        }

    }

    //------------------------------------------------暗屏幕 CurActivity end-----------------------------------------------------

    //-----------------------------------------------暗屏幕 end-----------------------------------------------------




    //------------------------------------------推送 begin---------------------------------------------------------
    public static int StartNotificationServices(){
        TlbbLog.d("----------------NativeManager:StartNotificationServices-----------------");
        if(MainActivity.CurActivity == null){
            TlbbLog.e("ERROR!!!-------------NativeManager:StartNotificationServices-----------------MainActivity.CurActivity = null!");
            return 0;
        }
        Intent serviceIntent = new Intent(MainActivity.CurActivity,NotificationService.class);
        MainActivity.CurActivity.startService(serviceIntent);
        TlbbLog.d("----------------NativeManager:StartNotificationServices-----------------");
        return 1;
    }

    /**
     * 供Unity调用~添加新的推送信息
     * @param paramJson
     */
    public void ShowNotification(String paramJson){
        TlbbLog.d("----------------NativeManager:ShowNotification-----------------");
        NotificationService.showNotification(paramJson);
    }

    /**
     * 供Unity调用~在进入游戏时清空推送信息
     * @param paramJson
     */
    public void CleanNotification(String paramJson){
        TlbbLog.d("----------------NativeManager:CleanNotification-----------------");
        NotificationService.cleanAll();
    }
    //------------------------------------------推送 end---------------------------------------------------------

















    //------------------------------------------------自动拉起安装----------------------------------------
    //by weina ~这个并没有设置成功~
    //一直报错:CrashReport:Post u3d crash AndroidJavaException java.lang.IllegalArgumentException:Failed to find configured root that contanins /storage/emulated/0/Android/data/com.tencent.tmgp.tstl/files/NewApkDownload/lwntest.apk
    public static void AutoInstall(String apkPath){
        if (CurActivity == null || TextUtils.isEmpty(apkPath)) {
            return;
        }

        File file = new File(apkPath);
        Intent intent = new Intent(Intent.ACTION_VIEW);//会根据用户的数据类型打开android系统相应的Activity。

        CurAndroidApiLevel = android.os.Build.VERSION.SDK_INT;
        if (CurAndroidApiLevel >= 24) // API Level min = 14
        {
            //provider authorities
            Uri apkUri = FileProvider.getUriForFile(CurActivity, "com.lwn.lwntest.fileprovider", file);
            //Granting Temporary Permissions to a URI
            intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
            intent.setDataAndType(apkUri, "application/vnd.android.package-archive");
        }else{

            //设置intent的数据类型是应用程序application
            intent.setDataAndType(Uri.fromFile(file), "application/vnd.android.package-archive");
            //为这个新apk开启一个新的activity栈
            intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);

        }
        //开始安装
        CurActivity.startActivity(intent);
        //关闭旧版本的应用程序的进程
        android.os.Process.killProcess(android.os.Process.myPid());
    }
    //------------------------------------------------自动拉起安装 end----------------------------------------

    /**
     * A native method that is implemented by the 'native-lib' native library,
     * which is packaged with this application.
     */
    public native String stringFromJNI();
}
