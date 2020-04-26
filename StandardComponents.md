# 标准构件库

为了更好的优化芯建建筑模型，将结构、物理、财务等相关信息更好的数据化，参数化，减小模型尺寸，最大尺度的共用模型，我们采用以下方式来构建。

## Standard Components 标准构件

使用标准构件库，可最大程度共用一个模型，并且附带Meta数据，以下为示例：
Meta信息为JSON, 可以描述该构件的物理、财务等信息，

|ID|命名|大小|模型|Meta|
|-|-|-|-|-|
| 1 | 芯板A | 12m x 2m x 0.15m | 1m x 1m x 1m 正方体 | height:1.5, core: 51, weight: 31 等
| 2 | 芯板P | 12m x 2m x 0.15m | 1m x 1m x 1m 正方体
| 3 | 芯板Q | 12m x 2m x 0.15m | 1m x 1m x 1m 正方体
| 4 | 芯板B | 12m x 2m x 0.15m | 1m x 1m x 1m 正方体
| 5 | 芯板D | 12m x 2m x 0.15m | 1m x 1m x 1m 正方体
| 6 | 芯板H | 12m x 2m x 0.15m | 1m x 1m x 1m 正方体
| 7 | 口型杆 |   |  1m x 1m x 1m 口型杆 | weight: 2
| 8 | 门型杆 |   |  1m x 1m x 1m 口型杆 | weight: 2
|....||||
| 20 | A型病床 | | A型病床 | ....

以上内容在Unity里可以是Prefab或者FBX, OBJ等文件。

可以通过标准构件库来构件模块(Module)

## Modules 建筑模块

通过使用 [Standard Components 标准构件] 来构建的模块，比如：2床病房、3床病房、4室2厅住宅、CB22-8或CB30-12，

模块保存成Prefab.

通过堆叠模块可构成建筑（Building）。

Prefab里包含了模块的结构信息、标准构件尺寸、动画。
通过每个构建的ID可以计算META信息，比如：重量、强度、成本。

Prefab里包含动画，使用Animator组件实现，有3种Stats:

1.  Static - 展示模块已经建好的样子
2.  Construction - 施工动画
3.  Decoration - 室内装饰、家具等动画(可选)

JSON数据结构
```
{
    components:[{
        model: "芯板B",
        transform:{
            position:{
                x:1.00,
                y:0,
                z:-20
            },
            rotation:{
                x:0,
                y:0,
                z:0
            },
            scale:{
                x: 12,
                y: 2,
                z: 0.15
            }
        },
        meta:{
            weight: 12,
        }
    },
    {
        ...
    }],
    meta:{
        beds: 2,
        ...
    }
}
```

## Building 建筑

1. 楼（通过模块数计算）
    * 单层面积
    * 层数
    * 大小
    * 建筑面积
    * 模块数
    * 电梯数
    * 等等

2. 功能（通过Meta计算）
    * 病房数
    * 负压参数
    * 等等

3. 特性（通过Meta计算）
    * 抗地震
    * 外墙保温

## Block 地块

1. GIS