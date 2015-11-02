using LinguisticLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LinguisticFeaturesAnalyzer.Data
{
    class ActiveSentence : Sentence, IActiveTextComponent, INotifyPropertyChanged
    {
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged("IsActive");

                foreach (var singleElement in this.OfType<IActiveTextComponent>())
                {
                    singleElement.IsActive = value;
                }
            }
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
