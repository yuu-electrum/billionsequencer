# Drawer

## 1. 概要

BillionSequencerのコンポーネントの一つであるDrawerの仕様です。サンプルデータはこのMarkdownファイルと同じフォルダの`samplesequence.json`を参照してください。

## 2. 仕様

Drawerは基本的にノーツを叩くことによって実行され、指定された視覚効果を描画します。現在`burst`のみ実装されており、それ以外のDrawerは現状実装予定はありません。Drawerつきのノーツの指定の方法については、[譜面フォーマットの仕様](譜面フォーマット.md)を参照してください。

### 2.2. burst

#### 2.2.1. 仕様

`burst`はノーツを叩くことに成功した場合にレーンの表示を消すDrawerです。

#### 2.2.2. パラメータ

##### boolean concentrates_lanes `required`

Drawer実行時に、レーンを中央に移動させるかどうか。trueの場合、ノーツの`lane`設定は無視されます。