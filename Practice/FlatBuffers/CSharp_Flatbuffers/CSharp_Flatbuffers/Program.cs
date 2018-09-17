using FlatBuffers;
using Fltest;
using System;
using System.IO;

namespace CSharp_Flatbuffers
{
    class Program
    {

        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;

            //test1:通过代码写bin 文件
            //InitFlatbuffers();
            //int res = int.Parse(Console.ReadLine());
            //if (res == 1) {
            //    path = path + "//..//..//..//FB_output//test.bin";
            //    ReadData(path);
            //}

            //test2:通过fbs和json文件生成bin文件后读取
            path = path + "//..//..//..//FB_output_auto//test.bin";
            ReadData(path);

            Console.ReadLine();
        }

        private static void ReadData(string path) {
            
            
            //FileStream file = null;
            //BinaryReader br = null;
            try
            {
                //file = new FileStream(path, FileMode.Open);
                //byte[] data = new byte[(int)file.Length];
                //br = new BinaryReader(file);
                //br.Read(data, 0, (int)file.Length);

                byte[] data = File.ReadAllBytes(path);

                ByteBuffer byteBuffer = new ByteBuffer(data);
                //byteBuffer.Position = 80;
                //byteBuffer.Position = ByteBufferUtil.GetSizePrefix(byteBuffer);
                RootType rootType = RootType.GetRootAsRootType(byteBuffer);
                Console.WriteLine("time = " + rootType.Time.ToString());
                Console.WriteLine("stateId = " + rootType.Stateid.ToString());

                int count = rootType.ItemsLength;
                Console.WriteLine("items.length = " + rootType.ItemsLength);
                for (int i = 0; i < count; i++)
                {
                    Person item = (Person)rootType.Items(i);
                    Console.WriteLine("item " + (i + 1) + "'s Id:" + item.Id);
                    Console.WriteLine("item " + (i + 1) + "'s name:" + item.Name);
                    Console.WriteLine("item " + (i + 1) + "'s code:" + item.Code);
                    Console.WriteLine("item " + (i + 1) + "'s carCount:" + item.CarListLength);
                    for (int j = 0; j < item.CarListLength; j++)
                    {
                        Car car = (Car)item.CarList(j);
                        Console.WriteLine("item " + (i + 1) + "'s 第" + (j + 1) + "个 car Id:" + car.Id);
                        Console.WriteLine("item " + (i + 1) + "'s 第" + (j + 1) + "个 car number:" + car.Number);
                        Console.WriteLine("item " + (i + 1) + "'s 第" + (j + 1) + "个 car des:" + car.Describle);
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally {
                //br.Close();
                //file.Close();
            }
            
        }


        private static void InitFlatbuffers() {
            FlatBufferBuilder fb = new FlatBufferBuilder(1);
            GenerateBin(fb);
        }

        private static void GenerateBin(FlatBufferBuilder fb) {
            

            StringOffset carStrOff = fb.CreateString("这是张三第一辆车");
            Offset<Car> carOff = Car.CreateCar(fb, 10001, (long)123456321, carStrOff);
            var carlist = new Offset<Car>[1];
            carlist[0] = carOff;
            VectorOffset carlistOff = Person.CreateCarListVector(fb, carlist);

            StringOffset nameOff = fb.CreateString("张三");
            Offset<Person> p1Off = Person.CreatePerson(fb, 1001, nameOff, (long)1222, false, carlistOff);

            StringOffset carStr1Off = fb.CreateString("这是李四第一辆车");
            StringOffset carStr2Off = fb.CreateString("这是李四第一辆车");
            StringOffset carStr3Off = fb.CreateString("这是李四第一辆车");

            StringOffset nameStr1Off = fb.CreateString("李四");

            Offset<Car> car1 = Car.CreateCar(fb, 10001, (long)123456001, carStr1Off);
            Offset<Car> car2 = Car.CreateCar(fb, 10002, (long)123456002, carStr2Off);
            Offset<Car> car3 = Car.CreateCar(fb, 10003, (long)123456003, carStr3Off);

            var carLists = new Offset<Car>[3];
            carLists[0] = car1;
            carLists[1] = car2;
            carLists[2] = car3;
            VectorOffset carlist2Off = Person.CreateCarListVector(fb, carLists);

            Offset<Person> p2Off = Person.CreatePerson(fb, 1002, nameStr1Off, (long)1123, false, carlist2Off);


            var items = new Offset<Person>[2];
            items[0] = p1Off;
            items[1] = p2Off;

            VectorOffset itemsOff = RootType.CreateItemsVector(fb, items);

            Offset<RootType> rootOff = RootType.CreateRootType(fb,itemsOff,404, (long)20180904);

            RootType.FinishRootTypeBuffer(fb, rootOff);
            
            //RootType rootType = RootType.GetRootAsRootType(fb.DataBuffer);
            //Console.WriteLine("time = " + rootType.Time.ToString());
            //Console.WriteLine("stateId = " + rootType.Stateid.ToString());
            Storage(fb);
        }

        private static void Storage(FlatBufferBuilder fb) {
            ByteBuffer data = fb.DataBuffer;
            MemoryStream ms = new MemoryStream(fb.DataBuffer.ToFullArray(), fb.DataBuffer.Position, fb.Offset);//这里很重要~要存储fb里面实际的数据长度和起始的位置，否自在读取的时候会错位
            string path = Environment.CurrentDirectory;
            Console.WriteLine("CurPath = " + path);
            //FileStream file = null;
            //BinaryWriter bw = null;
            try
            {
                //file = File.Create(path + "//..//..//..//FB_output//test.bin");
                //bw = new BinaryWriter(file);
                //bw.Write(dataArr, 0, dataArr.Length);
                File.WriteAllBytes(path + "//..//..//..//FB_output//test.bin", ms.ToArray());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally {
                //if (bw != null)
                //{
                //    bw.Close();
                //}
                //if (file != null) {
                //    file.Close();
                //}
                
            }
            

        }

    }
}
