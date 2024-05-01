using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace Game_Store.kind
{
    public class YourItemType
    {
        // 假设有一个属性作为示例
        public string Name { get; set; }

        // 可以添加更多属性和方法，根据您的需要
    }
    public class YourViewModel : INotifyPropertyChanged
    {
        // 使用private字段存储SelectedComboBoxItem属性的值
        private YourItemType _selectedComboBoxItem;
        // SelectedComboBoxItem公共属性
        public YourItemType SelectedComboBoxItem
        {
            get { return _selectedComboBoxItem; }
            set
            {
                // 只有当新值和旧值不同时，我们才更新属性值并触发PropertyChanged事件
                if (_selectedComboBoxItem != value)
                {
                    _selectedComboBoxItem = value;
                    OnPropertyChanged(nameof(SelectedComboBoxItem));
                }
            }
        }

        // 实现INotifyPropertyChanged接口所需的PropertyChanged事件
        public event PropertyChangedEventHandler PropertyChanged;

        // 触发PropertyChanged事件的辅助方法
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
