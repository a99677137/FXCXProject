// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Game.Table.Static
{

using global::System;
using global::FlatBuffers;
using System.Collections.Generic;

public struct UIResVO : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static UIResVO GetRootAsUIResVO(ByteBuffer _bb) { return GetRootAsUIResVO(_bb, new UIResVO()); }
  public static UIResVO GetRootAsUIResVO(ByteBuffer _bb, UIResVO obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public UIResVO __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Path { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetPathBytes() { return __p.__vector_as_arraysegment(6); }

  public static Offset<UIResVO> CreateUIResVO(FlatBufferBuilder builder,
      int Id = 0,
      StringOffset PathOffset = default(StringOffset)) {
    builder.StartObject(2);
    UIResVO.AddPath(builder, PathOffset);
    UIResVO.AddId(builder, Id);
    return UIResVO.EndUIResVO(builder);
  }

  public static void StartUIResVO(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(0, Id, 0); }
  public static void AddPath(FlatBufferBuilder builder, StringOffset PathOffset) { builder.AddOffset(1, PathOffset.Value, 0); }
  public static Offset<UIResVO> EndUIResVO(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<UIResVO>(o);
  }
};


}
