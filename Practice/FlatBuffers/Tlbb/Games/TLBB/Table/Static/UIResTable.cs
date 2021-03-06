// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Games.TLBB.Table.Static
{

using global::System;
using global::FlatBuffers;

public struct UIResTable : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static UIResTable GetRootAsUIResTable(ByteBuffer _bb) { return GetRootAsUIResTable(_bb, new UIResTable()); }
  public static UIResTable GetRootAsUIResTable(ByteBuffer _bb, UIResTable obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public UIResTable __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public UIResTableVO? Data(int j) { int o = __p.__offset(4); return o != 0 ? (UIResTableVO?)(new UIResTableVO()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<UIResTable> CreateUIResTable(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    UIResTable.AddData(builder, dataOffset);
    return UIResTable.EndUIResTable(builder);
  }

  public static void StartUIResTable(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<UIResTableVO>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<UIResTable> EndUIResTable(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<UIResTable>(o);
  }
  public static void FinishUIResTableBuffer(FlatBufferBuilder builder, Offset<UIResTable> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedUIResTableBuffer(FlatBufferBuilder builder, Offset<UIResTable> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
