package com.lwn.lwntest.normal;


import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.widget.TextView;

import com.lwn.lwntest.R;


public class TestSchemeActivity extends Activity {
	
	@Override 
	protected void onCreate(Bundle savedInstanceState){
		super.onCreate(savedInstanceState);
        this.setContentView(R.layout.activity_testscheme);
		
        
        TextView textview = (TextView)this.findViewById(R.id.testschemetitle);
        textview.setText(R.string.TestSchemeActivityTitle);
        
		Intent intent = getIntent();  
        String scheme = intent.getScheme();  
        Uri uri = intent.getData();  
        TlbbLog.d("scheme:"+scheme);  
        if (uri != null) {  
            String host = uri.getHost();  
            String dataString = intent.getDataString();  
            String id = uri.getQueryParameter("Id");  
            String name = uri.getQueryParameter("Name");  
            String path = uri.getPath();  
            String path1 = uri.getEncodedPath();  
            String queryString = uri.getQuery();  
            TlbbLog.d("host:"+host); 
            TlbbLog.d("dataString:"+dataString); 
            TlbbLog.d("Id:"+id);  
            TlbbLog.d("path:"+path);  
            TlbbLog.d("name:"+name);  
            TlbbLog.d("queryString:"+queryString); 
        }  
	}
}
