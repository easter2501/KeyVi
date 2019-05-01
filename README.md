# KeyVi - Windows用　キーボードユーティリティ

## 概要
KeyVi は、Windows上のすべてのアプリケーションにおいてvi likeな操作性を提供します。


## 動作環境
本プログラムは以下の環境にて動作確認を行っております。
・Microsoft WindowsXP Professional SP2　日本語版


## 要求事項
本プログラムの動作には、.NET Framework 2.0以降が必要です。

## インストール
KeyVi/keyvi/bin/Release/フォルダを任意のフォルダにコピーするだけでOKです。

## アンインストール
インストール時に作成したフォルダを削除するだけでOKです。
KeyViはレジストリを使用しておりません。


## 使用方法
KeyViを起動すると、タスクバーにアルファベット1文字のアイコンが表示されます。
アイコンはKeyViのモードによって以下のように切り替わります。
KeyViを終了するには、タスクバーのアイコンを右クリックして【終了】を選択します。
* 【I】　　　　　：　Inputモード
* 【C】　　　　　：　Commandモード
* 【X】（赤字）　：　Disableモード（KeyVi無効）
* 【X】（黒字）　：　Ignoreモード（無視）

## 各モードの説明
各モードは以下の操作によって切り替えることができます。
* Inputモードで【Esc】を押す　→　Commandモードに移行
* Commandモードで【Esc】を押す　→　【Esc】キーイベント発行
* Inputモードで【無変換】を押す　→　Commandモードに移行
* Commandモードで【i】を押す　→Inputモードに移行
* Commandモードで【:q】【Enter】を押す　→KeyVi無効モードに移行
KeyViの各モードの機能は以下の通りです。

* モード不問
  * 変換】キーを押すとIMEのON/OFFが切り替わります
　
* Inputモード
  * ふつうにキーを押した通りのキーイベントが発生します。（【Esc】【変換】【無変換】キーを除く）
　
* Commandモード
  * hjkl          ：　カーソル移動（←↓↑→）
  * Shift + hjkl  ：　選択しながらカーソル移動（Shift + ←↓↑→）
  * y             ：　コピー（Ctrl + c）　
  * yy            ：　行コピー（【↓】→ HOME → Shift + 【→】 + Home → Shift+End → Ctrl+c）
  * p             ：　ペースト（Ctrl + v）　
  * o             ：　現在行の下に行挿入し、Inputモードに移行（End → Enter → Inputモード）　
  * shift + o     ：　現在行の上に行挿入し、Inputモードに移行（Home → Enter → カーソル↑ → Inputモード）　
  * x             ：　一文字削除（Delete）　
  * Shift + x     ：　前の文字を一文字削除（Backspace）　
  * dd            ：　行削除（【↓】→ HOME → Shift + 【→】 + Home → Shift+End → Delete）
  * u             ：　直前操作の取り消し（Ctrl + z）　
  * Ctrl + f      ：　1画面下にスクロール（PageDown）
  * Ctrl + b      ：　1画面上にスクロール（PageUp）
  * Ctrl + h      ：　前の文字を一文字削除（Backspace）
  * 0             ：　行頭に移動（Home）
  * $(Shift + 4)  ：　行末に移動（End）
  * w             ：　次の単語に移動（Ctrl + 【→】）
  * b             ：　前の単語に移動（Ctrl + 【←】）
  * gg            ：　文頭に移動（Ctrl + Home）
  * Shift + g     ：　文末に移動（Ctrl + End）
  * Shift + i     ：　行頭に挿入（Home → Inputモード）
  * Shift + a     ：　行末に挿入（End → Inputモード）

* Disable（KeyVi無効）モード
  * KeyViが無効になり、キーバインドがノーマル状態になります。（【変換】キーを除く）
  * KeyViを再度有効にするには、タスクバーのアイコンを右クリックして【KeyViを有効にする】を選択します。
  * 一時的に誰かにキーボードを渡すときなどに使用してください。
　
* Ignore（無視）モード
  * 特定のアプリケーションがアクティブになっている場合にKeyViが無効になり、
  * キーバインドがノーマル状態になります。（【変換】キーを除く）
  * 設定画面の「アプリケーションごとの設定」でアプリケーション一覧に登録し、
  * 「Enable」欄のチェックが外れているアプリケーションが対象になります。
  * 対象のアプリケーションがアクティブになっていない場合、自動で従来のモードに戻ります。
　
## Copyright
本プログラムはフリーソフトウェアです。自由にご使用ください。なお、著作権は作者であるeasterが保有しています。


本プログラムは「vimouse」( http://sourceforge.net/projects/vimouse/ )をベースとしており、オリジナルのvimouseがMIT Licenceを適用しているため、それに倣い、MIT Licenceを適用いたします。本プログラムの再頒布および改変、流用等は、ライセンスに抵触しない範囲において自由です。


このソフトウェアを使用したことによって生じたすべての障害・損害・不具合等に関しては、私と私の関係者および私の所属するいかなる団体・組織とも、一切の責任を負いません。各自の責任においてご使用ください。


## 謝辞
本プログラムの雛形となったvimouseの作者であるGenki Takiuchi氏、本プログラムを開発するまでお世話になったXKeymacsの作者であるoishi氏、本プログラムの核となるWin32.csとSystemHotKey.csを提供されているCodeProjectにこの場を借りて感謝を申し上げます。





