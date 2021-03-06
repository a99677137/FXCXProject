// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Game.Table.Static
{

using global::System;
using global::FlatBuffers;
using System.Collections.Generic;

public struct Only4TestTable : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Only4TestTable GetRootAsOnly4TestTable(ByteBuffer _bb) { return GetRootAsOnly4TestTable(_bb, new Only4TestTable()); }
  public static Only4TestTable GetRootAsOnly4TestTable(ByteBuffer _bb, Only4TestTable obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Only4TestTable __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Only4TestVO? Data(int j) { int o = __p.__offset(4); return o != 0 ? (Only4TestVO?)(new Only4TestVO()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }

//LWN_Modify
public Dictionary<int, object> GetTableData() {
Dictionary<int, object> table = new Dictionary<int,object>();
for(int i=0;i<DataLength;i++){
Only4TestVO value = (Only4TestVO) Data(i);
int Id = System.Convert.ToInt32(value.Id);
table.Add(Id, value);
}
return table;
}
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<Only4TestTable> CreateOnly4TestTable(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    Only4TestTable.AddData(builder, dataOffset);
    return Only4TestTable.EndOnly4TestTable(builder);
  }

  public static void StartOnly4TestTable(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<Only4TestVO>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<Only4TestTable> EndOnly4TestTable(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<Only4TestTable>(o);
  }
  public static void FinishOnly4TestTableBuffer(FlatBufferBuilder builder, Offset<Only4TestTable> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedOnly4TestTableBuffer(FlatBufferBuilder builder, Offset<Only4TestTable> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
