﻿// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：Apache-2.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Tabs
    {
        [NotNull]
        private Tab? TabSet { get; set; }

        [NotNull]
        private Tab? TabSet2 { get; set; }

        private async Task AddTab(Tab tabset)
        {
            var text = $"Tab {tabset.Items.Count() + 1}";
            var item = new TabItem();
            var parameters = new Dictionary<string, object>
            {
                [nameof(TabItem.Text)] = text,
                [nameof(TabItem.IsActive)] = true,
                [nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
                {
                    var index = 0;
                    builder.OpenElement(index++, "div");
                    builder.AddContent(index++, $"我是新建的 Tab 名称是 {text}");
                    builder.CloseElement();
                })
            };
            var _ = item.SetParametersAsync(ParameterView.FromDictionary(parameters));
            await tabset.Add(item);
        }

        private string? RemoveEndableString => (TabSet?.Items.Count() > 4) ? null : "true";

        private async Task RemoveTab(Tab tabset)
        {
            if (tabset.Items.Count() > 4)
            {
                var item = tabset.Items.Last();
                await tabset.Remove(item);
            }
        }

        private Placement BindPlacement = Placement.Top;

        private void SetPlacement(Placement placement)
        {
            BindPlacement = placement;
        }

        private IEnumerable<MenuItem> GetSideMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem() { Text = "计数器"  },
                new MenuItem() { Text = "天气预报" }
            };
        }

        [NotNull]
        private Tab? TabSetMenu { get; set; }

        private async Task OnClickMenuItem(MenuItem item)
        {
            var text = item.Text;
            var tabItem = TabSetMenu.Items.FirstOrDefault(i => i.Text == text);
            if (tabItem == null) await AddTabItem(text ?? "");
            else await TabSetMenu.ActiveTab(tabItem);
        }

        private async Task AddTabItem(string text)
        {
            var item = new TabItem();
            var parameters = new Dictionary<string, object>
            {
                [nameof(TabItem.Text)] = text,
                [nameof(TabItem.IsActive)] = true,
                [nameof(TabItem.ChildContent)] = text == "计数器" ? DynamicComponent.CreateComponent<Counter>().Render() : DynamicComponent.CreateComponent<FetchData>().Render()
            };
            var _ = item.SetParametersAsync(ParameterView.FromDictionary(parameters));
            await TabSetMenu.Add(item);
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "IsBorderCard",
                Description = "是否为带边框卡片样式",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCard",
                Description = "是否为卡片样式",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Placement",
                Description = "设置标签位置",
                Type = "Placement",
                ValueList = "Top|Right|Bottom|Left",
                DefaultValue = "Top"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "设置标签高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "ShowClose",
                Description = "是否显示关闭按钮",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowExtendButtons",
                Description = "是否显示扩展按钮",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "TabItem 集合",
                Type = "IEnumerable<TabItemBase>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "ChildContent 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        /// <summary>
        /// 获得方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            // TODO: 移动到数据库中
            new MethodItem() {
                Name = "Add",
                Description = "添加 TabItem 到 Tab 中",
                Parameters = "TabItem",
                ReturnValue = " — "
            },
            new MethodItem() {
                Name = "Remove",
                Description = "移除 TabItem",
                Parameters = "TabItem",
                ReturnValue = " — "
            },
            new MethodItem() {
                Name = "ReActiveTab",
                Description = "切换后回调此方法",
                Parameters = " — ",
                ReturnValue = " — "
            }
        };
    }
}
