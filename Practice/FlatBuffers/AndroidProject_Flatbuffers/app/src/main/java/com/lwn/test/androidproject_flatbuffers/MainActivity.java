package com.lwn.test.androidproject_flatbuffers;


import android.os.Bundle;
import android.os.Environment;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.google.flatbuffers.FlatBufferBuilder;
import com.google.flatbuffers.test.Car;
import com.google.flatbuffers.test.Person;
import com.google.flatbuffers.test.RootType;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;

public class MainActivity extends AppCompatActivity implements View.OnClickListener{
    private Button testBtn;
    private TextView tvflat;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        testBtn = (Button)findViewById(R.id.bt_flat);
        tvflat = (TextView) findViewById(R.id.tv_flat);
        testBtn.setOnClickListener(this);
        initflat();
    }

    @Override
    public void onClick(View v){
        int id = v.getId();
        switch (id){
            case R.id.bt_flat:
                getFlat();
                break;
            default:
                break;
        }

    }

    //序列化数据作用类似于json中的构建JSONobject对象。
private void initflat() {
        FlatBufferBuilder builder = new FlatBufferBuilder();
        //从里往外构建，最里层是String;
        //每个对象存的都是偏移量类似于指针
        //命名_id指偏移量offset
        int describle_id = builder.createString("这是张三的第一辆车");
        int car_id = Car.createCar(builder, 10001, 123456321L, describle_id);
        int[] cars = {car_id};
        int cars_id = Person.createCarListVector(builder, cars);
        int name_id = builder.createString("张三");
        int person1_id = Person.createPerson(builder, 1001, name_id, 1222L, true, cars_id);
        //items中person1建立完成。
        int describle1_id = builder.createString("这是李四的第一辆车");
        int car1_id = Car.createCar(builder, 10001, 123456001L, describle1_id);
        int describle2_id = builder.createString("这是李四的第二辆车");
        int car2_id = Car.createCar(builder, 10002, 123456002L, describle2_id);
        int describle3_id = builder.createString("这是李四的第三辆车");
        int car3_id = Car.createCar(builder, 10003, 123456003L, describle3_id);
        int[] cars2 = {car1_id, car2_id, car3_id};
        int cars2_id = Person.createCarListVector(builder, cars2);
        int name2_id = builder.createString("李四");
        int person2_id = Person.createPerson(builder, 1002, name2_id, 1223L, false, cars2_id);
        //items中person2建立完成
        int[] persons = {person1_id, person2_id};
        int persons_id = RootType.createItemsVector(builder, persons);
        int roottype_id = RootType.createRootType(builder, persons_id, 404, 20161127L);
        RootType.finishRootTypeBuffer(builder, roottype_id);
        //根类型RootType构建完成。得到builder可获取到bytebuffer然后即可存入文件，也可直接发送到网络。
        //此处存入文件中。
        storageFlat(builder);
    }
    //存储数据到文件。
    private void storageFlat(FlatBufferBuilder builder) {
        ByteBuffer data = builder.dataBuffer();//得到底层存储二进制数据的ByteBuffer
        File sdcard = Environment.getExternalStorageDirectory();
        File file = new File(sdcard, "Flattest.bin");
        if (file.exists()) {
            file.delete();
        }
        FileOutputStream out = null;
        FileChannel channel = null;
        try {
            //file.createNewFile();
            out = new FileOutputStream(file);
            channel = out.getChannel();
            while (data.hasRemaining()) {
                try {
                    channel.write(data);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                if (null != channel) {
                    channel.close();
                }
                if (null != out) {
                    out.close();
                }

            } catch (Exception e2) {

            }
        }
    }


    //反序列化flatbuffer 即解析
    private void getFlat() {
        File file = new File(Environment.getExternalStorageDirectory(), "Flattest.bin");
        FileInputStream fin = null;
        FileChannel readChannel = null;
        try {
            fin = new FileInputStream(file);
            ByteBuffer byteBuffer = ByteBuffer.allocate(1024);
            readChannel = fin.getChannel();
            int readbytes = 0;
            while ((readbytes = readChannel.read(byteBuffer)) != -1) {
                System.out.printf("读到：" + readbytes + "个数据");
            }
            byteBuffer.flip();//positon回绕，准备从bytebuffer中读取数据
            RootType rootType = RootType.getRootAsRootType(byteBuffer);
            //测试读取的数据与写入的数据是否一致
            StringBuilder sb = new StringBuilder();
            sb.append("RootType--->" + "id:" + rootType.stateid() + "   time：" + rootType.time() + "\n");
            Person person1 = rootType.items(0);
            Person person2 = rootType.items(1);
            Car car = person1.carList(0);
            Car car1 = person2.carList(0);
            Car car2 = person2.carList(1);
            Car car3 = person2.carList(2);
            sb.append("person1-->" + "id:" + person1.id() + "  name:" + person1.name() + "  code:" + person1.code()
                    + "  isVip:" + person1.isVip() + "\n");
            sb.append("car->" + "id:" + car.id() + "   number:" + car.number() + "  describle:" + car.describle() + "\n");
            sb.append("Person2-->" + "id:" + person2.id() + "  name:" + person2.name() + "  code:" + person2.code()
                    + "  isVip:" + person2.isVip() + "\n");
            sb.append("car1->" + "id:" + car1.id() + "   number:" + car1.number() + "  describle:" + car1.describle() + "\n");
            sb.append("car2->" + "id:" + car2.id() + "   number:" + car2.number() + "  describle:" + car2.describle() + "\n");
            sb.append("car3->" + "id:" + car3.id() + "   number:" + car3.number() + "  describle:" + car3.describle() + "\n");
            tvflat.setText(sb.toString());

        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                if (null != readChannel) {
                    readChannel.close();
                }
                if (null != fin) {
                    fin.close();
                }
            } catch (Exception e2) {
                e2.printStackTrace();
            }
        }

    }

}
