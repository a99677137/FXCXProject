package com.lwn.lwntest.normal;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.text.method.ScrollingMovementMethod;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.lwn.lwntest.R;

import java.io.BufferedReader;
import java.io.InputStreamReader;

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

    @Override
    protected void onCreate( Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_logcat);

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

    }

    Handler mTimeHandler = new Handler() {
        public void handleMessage(android.os.Message message) {
            if (message.what == 0) {
                msg.append (stringB.toString()); //View.ininvalidate()
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
            mLogcatProc = Runtime.getRuntime().exec( new String[]{"logcat",TlbbLog.getTag()+":D *:S"});
            reader = new BufferedReader(new InputStreamReader(mLogcatProc.getInputStream()));

            String line = "";
            while((line = reader.readLine()) != null){
//                if(line.contains("Lwn")){
//                    TlbbLog.d(line);
                    stringB.append("\n"+line );
//                }
            }
        }catch (Exception e){
            e.printStackTrace();
        }
    }

}
