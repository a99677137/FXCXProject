package com.lwn.lwntest.normal;

import android.app.Activity;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Looper;
import android.text.method.ScrollingMovementMethod;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.lwn.lwntest.MainActivity;
import com.lwn.lwntest.R;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

/**
 * Created by liweina on 2018/10/17.
 */

public class LogCatActivity extends Activity implements  Runnable {

    private TextView msg;
    Process mLogcatProc = null;
    BufferedReader reader = null;
    private boolean threadIsStart = false;
    StringBuilder stringB = null;
    int num = 0;
    File logFile = null;
    FileWriter fileWriter = null;
    String logFilePath = "";

    @Override
    protected void onCreate( Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_logcat);
        android.os.Process.myPid();

        TlbbLog.d("Process Id = " + android.os.Process.myPid());
        msg = this.findViewById(R.id.MsgContent);
        msg.setText("Start!");
        msg.setMovementMethod(ScrollingMovementMethod.getInstance());

        stringB = new StringBuilder();
        Button startThread = (Button) this.findViewById(R.id.StartThread);
        startThread.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(threadIsStart){
                    return;
                }
                TlbbLog.d("LogCatActivity:Start Thread!");
                 new Thread((Runnable) LogCatActivity.this).start();
            }
        });

        Button createLog = (Button) this.findViewById(R.id.CreateLog);
        createLog.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                num +=1;
                TlbbLog.d("LogCatActivity:Create Log! Num = " + num);
            }
        });


//        try{
//            this.runOnUiThread(new Runnable()
//            {
//                @Override
//                public void run() {
//                    msg.append(stringB.toString());
//                }
//            });
//        }catch(Exception e){
//            e.printStackTrace();
//        }

        mTimeHandler.sendEmptyMessageDelayed(0, 1000);

        logFilePath = getApplicationContext().getExternalFilesDir(Environment.DIRECTORY_DOCUMENTS).getAbsolutePath();
        Date date = new Date();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd-HH-mm");
        try {
            logFile = new File(logFilePath+"/AndroidLog-"+ sdf.format(date)+".txt");
            TlbbLog.d("LogFilepath = " + logFile.getPath());
            if(!logFile.exists()){
                logFile.createNewFile();
            }
            fileWriter = new FileWriter(logFile,true);
            fileWriter.write("AndroidLogCreateFile:" +sdf.format(date) +"\n");
            fileWriter.flush();
        }catch (Exception e){
            TlbbLog.d("LogCatActivity:onCreate Exception: " + e.toString());
        }

    }

    private void refreshAlarmView(TextView textView,String msg){
        textView.append(msg);
        int offset=textView.getLineCount()*textView.getLineHeight();
        if(offset>(textView.getHeight()-textView.getLineHeight()-20)){
            textView.scrollTo(0,offset-textView.getHeight()+textView.getLineHeight());
        }
    }



    Handler mTimeHandler = new Handler(Looper.getMainLooper()) {
        public void handleMessage(android.os.Message message) {
            if (message.what == 0) {
                try {
                    msg.append (stringB.toString()); //View.ininvalidate()
                    //refreshAlarmView(msg,stringB.toString());
                    fileWriter.write(stringB.toString());
                    fileWriter.flush();
                }catch(Exception e){
                    TlbbLog.d("LogCatActivity:handleMessage Exception: " + e.toString());
                }
                sendEmptyMessageDelayed(0, 1000);
                stringB.delete(0,stringB.length());
            }
        }
    };

    @Override
    public void run(){
        threadIsStart = true;

        TlbbLog.d("!!! ThreadRun!!!!");
        try{
//            mLogcatProc = Runtime.getRuntime().exec( new String[]{"logcat",TlbbLog.getTag()+":D *:S"});
            mLogcatProc = Runtime.getRuntime().exec( new String[]{"logcat","|find","\"" +  MainActivity.ProcessId + "\""});
            reader = new BufferedReader(new InputStreamReader(mLogcatProc.getInputStream()));

            String line = "";
            while((line = reader.readLine()) != null){
                    stringB.append("\n"+line );
            }
        }catch (Exception e){
            e.printStackTrace();
        }
    }

    @Override
    protected void onDestroy(){
        super.onDestroy();
        try {
            mLogcatProc.destroy();
            mLogcatProc = null;
            reader.close();
            stringB.setLength(0);
            stringB = null;
            reader = null;
            mTimeHandler.removeMessages(0);
            mTimeHandler = null;
            fileWriter.close();
            fileWriter = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
