using Poly.Geometry;
using PolySketch.Layering.RenderNode;

namespace PolySketch.UI.Canvas.Interfaces
{
    public interface ICanvasLayerContainer
    {
        event SingleTouchDelegate SingleTouchStarted;

        event SingleTouchDelegate SingleTouchMoved;

        event SingleTouchDelegate SingleTouchEnded;

        event SingleTouchDelegate SingleTouchCancelled;

        event DoubleTouchDelegate DoubleTouchStarted;

        event DoubleTouchDelegate DoubleTouchMoved;

        event DoubleTouchDelegate DoubleTouchCancelled;

        event DoubleTouchDelegate DoubleTouchEnded;

        void SetRotation( float angle );

        void SetScale( float scale );

        PVector GetPosition();

        void SetPosition( PVector pos );

        void AddChild( IRenderNode node );

        void RemoveChild( IRenderNode node );

        void RemoveAllChildren();

        object GetLayer();

        object GetMainNode();
    }
}