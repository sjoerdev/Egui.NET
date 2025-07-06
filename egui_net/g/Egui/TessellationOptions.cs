#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct TessellationOptions : IEquatable<TessellationOptions> {
        public bool Feathering;
        public float FeatheringSizeInPixels;
        public bool CoarseTessellationCulling;
        public bool PrerasterizedDiscs;
        public bool RoundTextToPixels;
        public bool RoundLineSegmentsToPixels;
        public bool RoundRectsToPixels;
        public bool DebugPaintClipRects;
        public bool DebugPaintTextRects;
        public bool DebugIgnoreClipRects;
        public float BezierTolerance;
        public float Epsilon;
        public bool ParallelTessellation;
        public bool ValidateMeshes;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_bool(Feathering);
            serializer.serialize_f32(FeatheringSizeInPixels);
            serializer.serialize_bool(CoarseTessellationCulling);
            serializer.serialize_bool(PrerasterizedDiscs);
            serializer.serialize_bool(RoundTextToPixels);
            serializer.serialize_bool(RoundLineSegmentsToPixels);
            serializer.serialize_bool(RoundRectsToPixels);
            serializer.serialize_bool(DebugPaintClipRects);
            serializer.serialize_bool(DebugPaintTextRects);
            serializer.serialize_bool(DebugIgnoreClipRects);
            serializer.serialize_f32(BezierTolerance);
            serializer.serialize_f32(Epsilon);
            serializer.serialize_bool(ParallelTessellation);
            serializer.serialize_bool(ValidateMeshes);
            serializer.decrease_container_depth();
        }

        internal static TessellationOptions Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            TessellationOptions obj = new TessellationOptions {
            	Feathering = deserializer.deserialize_bool(),
            	FeatheringSizeInPixels = deserializer.deserialize_f32(),
            	CoarseTessellationCulling = deserializer.deserialize_bool(),
            	PrerasterizedDiscs = deserializer.deserialize_bool(),
            	RoundTextToPixels = deserializer.deserialize_bool(),
            	RoundLineSegmentsToPixels = deserializer.deserialize_bool(),
            	RoundRectsToPixels = deserializer.deserialize_bool(),
            	DebugPaintClipRects = deserializer.deserialize_bool(),
            	DebugPaintTextRects = deserializer.deserialize_bool(),
            	DebugIgnoreClipRects = deserializer.deserialize_bool(),
            	BezierTolerance = deserializer.deserialize_f32(),
            	Epsilon = deserializer.deserialize_f32(),
            	ParallelTessellation = deserializer.deserialize_bool(),
            	ValidateMeshes = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is TessellationOptions other && Equals(other);

        public static bool operator ==(TessellationOptions left, TessellationOptions right) => Equals(left, right);

        public static bool operator !=(TessellationOptions left, TessellationOptions right) => !Equals(left, right);

        public bool Equals(TessellationOptions other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Feathering.Equals(other.Feathering)) return false;
            if (!FeatheringSizeInPixels.Equals(other.FeatheringSizeInPixels)) return false;
            if (!CoarseTessellationCulling.Equals(other.CoarseTessellationCulling)) return false;
            if (!PrerasterizedDiscs.Equals(other.PrerasterizedDiscs)) return false;
            if (!RoundTextToPixels.Equals(other.RoundTextToPixels)) return false;
            if (!RoundLineSegmentsToPixels.Equals(other.RoundLineSegmentsToPixels)) return false;
            if (!RoundRectsToPixels.Equals(other.RoundRectsToPixels)) return false;
            if (!DebugPaintClipRects.Equals(other.DebugPaintClipRects)) return false;
            if (!DebugPaintTextRects.Equals(other.DebugPaintTextRects)) return false;
            if (!DebugIgnoreClipRects.Equals(other.DebugIgnoreClipRects)) return false;
            if (!BezierTolerance.Equals(other.BezierTolerance)) return false;
            if (!Epsilon.Equals(other.Epsilon)) return false;
            if (!ParallelTessellation.Equals(other.ParallelTessellation)) return false;
            if (!ValidateMeshes.Equals(other.ValidateMeshes)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Feathering.GetHashCode();
                value = 31 * value + FeatheringSizeInPixels.GetHashCode();
                value = 31 * value + CoarseTessellationCulling.GetHashCode();
                value = 31 * value + PrerasterizedDiscs.GetHashCode();
                value = 31 * value + RoundTextToPixels.GetHashCode();
                value = 31 * value + RoundLineSegmentsToPixels.GetHashCode();
                value = 31 * value + RoundRectsToPixels.GetHashCode();
                value = 31 * value + DebugPaintClipRects.GetHashCode();
                value = 31 * value + DebugPaintTextRects.GetHashCode();
                value = 31 * value + DebugIgnoreClipRects.GetHashCode();
                value = 31 * value + BezierTolerance.GetHashCode();
                value = 31 * value + Epsilon.GetHashCode();
                value = 31 * value + ParallelTessellation.GetHashCode();
                value = 31 * value + ValidateMeshes.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
