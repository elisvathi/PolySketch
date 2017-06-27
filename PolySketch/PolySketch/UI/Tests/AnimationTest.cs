using Ninject;
using Poly.NinjectModules.Kernels;
using System;

using Xamarin.Forms;

namespace PolySketch.UI.Tests
{
    public class AnimationTest : ContentPage
    {
        private int actual = -1;

        private KernelService Service { get; set; }
        private TestCocosPage testPage { get { return Service.ActiveKernel.Get<TestCocosPage>(); } }
        private TestClassCounter testnumber { get { return Service.ActiveKernel.Get<TestClassCounter>(); } }
        private int numFiles { get { return Service.NumKernels; } }

        public AnimationTest( KernelService serv )
        {
            this.Service = serv;
            UpdateChildren();
        }

        public void UpdateChildren()
        {
            Button button = new Button
            {
                Text = "ADD SCENE"
            };
            Button switchScene = new Button
            {
                Text = "Switch"
            };
            switchScene.Clicked += SwitchScene;
            Label l = new Label
            {
                Text = "Teksti qe do animohet"
            };
            button.Clicked += ( sender , e ) => klikimButoni(l);

            var lay = new StackLayout();
            lay.Children.Add(button);
            lay.Children.Add(switchScene);
            lay.Children.Add(l);

            if ( actual >= 0 )
            {
                Label textTest = new Label();
                if ( testPage != null )
                {
                    textTest.Text = "THE NUMBER IS: " + testnumber.TestNumber;
                    Button increse = new Button { Text = "INCREASe" };
                    increse.Clicked += IncreaseNumber;
                    lay.Children.Add(increse);
                } else
                {
                    textTest.Text = "Null Text";
                }
                lay.Children.Add(textTest);
            }
            Content = lay;
        }

        private void IncreaseNumber( object sender , EventArgs e )
        {
            testnumber.TestNumber++;
            UpdateChildren();
        }

        private void SwitchScene( object sender , EventArgs e )
        {
            if ( numFiles > 0 )
            {
                if ( actual == numFiles - 1 )
                { actual = 0; } else
                { actual++; }
            }
            Service.SetActive(actual);
            UpdateChildren();
        }

        private void klikimButoni( Label l )
        {
            Service.OnAddFile();
            UpdateChildren();
        }
    }
}