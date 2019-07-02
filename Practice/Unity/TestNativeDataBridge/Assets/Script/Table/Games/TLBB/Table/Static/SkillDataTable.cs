// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Games.TLBB.Table.Static
{

using global::System;
using global::FlatBuffers;
using System.Collections.Generic;

public struct SkillDataTable : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static SkillDataTable GetRootAsSkillDataTable(ByteBuffer _bb) { return GetRootAsSkillDataTable(_bb, new SkillDataTable()); }
  public static SkillDataTable GetRootAsSkillDataTable(ByteBuffer _bb, SkillDataTable obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public SkillDataTable __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public SkillDataTableVO? Data(int j) { int o = __p.__offset(4); return o != 0 ? (SkillDataTableVO?)(new SkillDataTableVO()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }

//LWN_Modify
public Dictionary<int, object> GetTableData() {
Dictionary<int, object> table = new Dictionary<int,object>();
for(int i=0;i<DataLength;i++){
SkillDataTableVO value = (SkillDataTableVO) Data(i);
int Id = System.Convert.ToInt32(value.Id);
table.Add(Id, value);
}
return table;
}
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<SkillDataTable> CreateSkillDataTable(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    SkillDataTable.AddData(builder, dataOffset);
    return SkillDataTable.EndSkillDataTable(builder);
  }

  public static void StartSkillDataTable(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<SkillDataTableVO>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<SkillDataTable> EndSkillDataTable(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<SkillDataTable>(o);
  }
  public static void FinishSkillDataTableBuffer(FlatBufferBuilder builder, Offset<SkillDataTable> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedSkillDataTableBuffer(FlatBufferBuilder builder, Offset<SkillDataTable> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}