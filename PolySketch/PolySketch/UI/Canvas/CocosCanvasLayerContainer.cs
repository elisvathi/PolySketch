using CocosSharp;
using Poly.Geometry;
using PolySketch.Helpers;
using PolySketch.Layering.RenderNode;
using PolySketch.UI.Canvas.Interfaces;
using System.Collections.Generic;

namespace PolySketch.UI.Canvas
{
    public class CocosCanvasLayerContainer : ICanvasLayerContainer
    {
        private MainLayer _Layer;

        private CCNode MainNode { get { return _Layer.MainNode; } }

        public event SingleTouchDelegate SingleTouchStarted;

        public event SingleTouchDelegate SingleTouchMoved;

        public event SingleTouchDelegate SingleTouchEnded;

        public event SingleTouchDelegate SingleTouchCancelled;

        public event DoubleTouchDelegate DoubleTouchStarted;

        public event DoubleTouchDelegate DoubleTouchMoved;

        public event DoubleTouchDelegate DoubleTouchCancelled;

        public event DoubleTouchDelegate DoubleTouchEnded;

        public CocosCanvasLayerContainer( CCLayer layer )
        {
            _Layer = layer as MainLayer;
            var eventListener = new CCEventListenerTouchAllAtOnce();
            eventListener.OnTouchesBegan += TouchesBegan;
            eventListener.OnTouchesMoved += TouchesMoved;
            eventListener.OnTouchesEnded += TouchesEnded;
            eventListener.OnTouchesCancelled += TouchesCancelled;
            _Layer.AddEventListener(eventListener);
        }

        //Single touch Invokers
        protected virtual void OnSingleTouchStarted( CCTouch touch1 )
        {
            SingleTouchStarted?.Invoke(touch1.GetTouch());
        }

        protected virtual void OnSingleTouchMoved( CCTouch touch1 )
        {
            SingleTouchMoved?.Invoke(touch1.GetTouch());
        }

        protected virtual void OnSingleTouchCancelled( CCTouch touch1 )
        {
            SingleTouchCancelled?.Invoke(touch1.GetTouch());
        }

        protected virtual void OnSingleTouchEnded( CCTouch touch1 )
        {
            SingleTouchEnded?.Invoke(touch1.GetTouch());
        }

        //Double touch invokers
        protected virtual void OnDoubleTouchStarted( CCTouch touch1 , CCTouch touch2 )
        {
            DoubleTouchStarted?.Invoke(new PDoubleTouch(touch1.GetTouch() , touch2.GetTouch()));
        }

        protected virtual void OnDoubleTouchEnded( CCTouch touch1 , CCTouch touch2 )
        {
            DoubleTouchEnded?.Invoke(new PDoubleTouch(touch1.GetTouch() , touch2.GetTouch()));
        }

        protected virtual void OnDoubleTouchMoved( CCTouch touch1 , CCTouch touch2 )
        {
            DoubleTouchMoved?.Invoke(new PDoubleTouch(touch1.GetTouch() , touch2.GetTouch()));
        }

        protected virtual void OnDoubleTouchCancelled( CCTouch touch1 , CCTouch touch2 )
        {
            DoubleTouchCancelled?.Invoke(new PDoubleTouch(touch1.GetTouch() , touch2.GetTouch()));
        }

        private void TouchesCancelled( List<CCTouch> touches , CCEvent arg2 )
        {
            if ( touches.Count>0 )
            {
                if ( LayerContains(touches[0]) )
                {
                    OnSingleTouchCancelled(touches[0]);
                }
            } else if ( touches.Count > 2 )
            {
                if ( LayerContains(touches[0]) && LayerContains(touches[1]) )
                {
                    OnDoubleTouchCancelled(touches[0] , touches[1]);
                }
            }
        }

        private void TouchesEnded( List<CCTouch> touches , CCEvent arg2 )
        {
            if ( touches.Count >0 )
            {
                if ( LayerContains(touches[0]) )
                {
                    OnSingleTouchEnded(touches[0]);
                }
            } else if ( touches.Count > 2 )
            {
                if ( LayerContains(touches[0]) && LayerContains(touches[1]) )
                {
                    OnDoubleTouchEnded(touches[0] , touches[1]);
                }
            }
        }

        private void TouchesMoved( List<CCTouch> touches , CCEvent arg2 )
        {
            if ( touches.Count >0 )
            {
                if ( LayerContains(touches[0]) )
                {
                    OnSingleTouchMoved(touches[0]);
                }
            } else if ( touches.Count > 2 )
            {
                if ( LayerContains(touches[0]) && LayerContains(touches[1]) )
                {
                    OnDoubleTouchMoved(touches[0] , touches[1]);
                }
            }
        }

        private void TouchesBegan( List<CCTouch> touches , CCEvent arg2 )
        {
            if ( touches.Count >0 )
            {
                if ( LayerContains(touches[0]) )
                {
                    OnSingleTouchStarted(touches[0]);
                }
            } else if ( touches.Count > 2 )
            {
                if ( LayerContains(touches[0]) && LayerContains(touches[1]) )
                {
                    OnDoubleTouchStarted(touches[0] , touches[1]);
                }
            }
        }

        private bool LayerContains( PVector p )
        {
            return _Layer.BoundingBox.ContainsPoint(p.GetPoint());
        }

        private bool LayerContains( CCTouch touch )
        {
            return LayerContains(touch.GetTouch().ActualPoint);
        }

        public void AddChild( IRenderNode node )
        {
            MainNode.AddChild(( CCNode ) node.GetNode());
        }

        public object GetLayer()
        {
            return _Layer;
        }

        public PVector GetPosition()
        {
            return MainNode.Position.GetVector();
        }

        public void RemoveAllChildren()
        {
            MainNode.RemoveAllChildren(true);
        }

        public void RemoveChild( IRenderNode node )
        {
            MainNode.RemoveChild(( CCNode ) node.GetNode());
        }

        public void SetPosition( PVector pos )
        {
            MainNode.Position = pos.GetPoint();
        }

        public void SetRotation( float angle )
        {
            MainNode.Rotation = angle;
        }

        public void SetScale( float scale )
        {
            MainNode.Scale = scale;
        }

        public object GetMainNode()
        {
            return MainNode;
        }
    }
}