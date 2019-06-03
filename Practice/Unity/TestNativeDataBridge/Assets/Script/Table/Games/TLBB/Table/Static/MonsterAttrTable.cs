// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Games.TLBB.Table.Static
{

using global::System;
using global::FlatBuffers;
using System.Collections.Generic;

public struct MonsterAttrTable : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static MonsterAttrTable GetRootAsMonsterAttrTable(ByteBuffer _bb) { return GetRootAsMonsterAttrTable(_bb, new MonsterAttrTable()); }
  public static MonsterAttrTable GetRootAsMonsterAttrTable(ByteBuffer _bb, MonsterAttrTable obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public MonsterAttrTable __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public MonsterAttrTableVO? Data(int j) { int o = __p.__offset(4); return o != 0 ? (MonsterAttrTableVO?)(new MonsterAttrTableVO()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }

//LWN_Modify
public Dictionary<int, object> GetTableData() {
Dictionary<int, object> table = new Dictionary<int,object>();
for(int i=0;i<DataLength;i++){
MonsterAttrTableVO value = (MonsterAttrTableVO) Data(i);
int Id = System.Convert.ToInt32(value.Id);
table.Add(Id, value);
}
return table;
}
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<MonsterAttrTable> CreateMonsterAttrTable(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    MonsterAttrTable.AddData(builder, dataOffset);
    return MonsterAttrTable.EndMonsterAttrTable(builder);
  }

  public static void StartMonsterAttrTable(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<MonsterAttrTableVO>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<MonsterAttrTable> EndMonsterAttrTable(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<MonsterAttrTable>(o);
  }
  public static void FinishMonsterAttrTableBuffer(FlatBufferBuilder builder, Offset<MonsterAttrTable> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedMonsterAttrTableBuffer(FlatBufferBuilder builder, Offset<MonsterAttrTable> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
