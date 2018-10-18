// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Games.TLBB.Table.Static
{

using global::System;
using global::FlatBuffers;

public struct UIResTableVO : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static UIResTableVO GetRootAsUIResTableVO(ByteBuffer _bb) { return GetRootAsUIResTableVO(_bb, new UIResTableVO()); }
  public static UIResTableVO GetRootAsUIResTableVO(ByteBuffer _bb, UIResTableVO obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public UIResTableVO __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Info { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetInfoBytes() { return __p.__vector_as_arraysegment(6); }
  public string NorResPath { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetNorResPathBytes() { return __p.__vector_as_arraysegment(8); }
  public string LowResPath { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetLowResPathBytes() { return __p.__vector_as_arraysegment(10); }
  public int State { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<UIResTableVO> CreateUIResTableVO(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset InfoOffset = default(StringOffset),
      StringOffset NorResPathOffset = default(StringOffset),
      StringOffset LowResPathOffset = default(StringOffset),
      int State = 0) {
    builder.StartObject(5);
    UIResTableVO.AddState(builder, State);
    UIResTableVO.AddLowResPath(builder, LowResPathOffset);
    UIResTableVO.AddNorResPath(builder, NorResPathOffset);
    UIResTableVO.AddInfo(builder, InfoOffset);
    UIResTableVO.AddId(builder, Id);
    return UIResTableVO.EndUIResTableVO(builder);
  }

  public static void StartUIResTableVO(FlatBufferBuilder builder) { builder.StartObject(5); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddInfo(FlatBufferBuilder builder, StringOffset InfoOffset) { builder.AddOffset(1, InfoOffset.Value, 0); }
  public static void AddNorResPath(FlatBufferBuilder builder, StringOffset NorResPathOffset) { builder.AddOffset(2, NorResPathOffset.Value, 0); }
  public static void AddLowResPath(FlatBufferBuilder builder, StringOffset LowResPathOffset) { builder.AddOffset(3, LowResPathOffset.Value, 0); }
  public static void AddState(FlatBufferBuilder builder, int State) { builder.AddInt(4, State, 0); }
  public static Offset<UIResTableVO> EndUIResTableVO(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<UIResTableVO>(o);
  }
};


}