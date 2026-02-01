# UsefulSystems
便利な各種システム  
内容
* フェードシステム
* サウンドシステム

### ブランチ説明
* UniTask
  * UniTaskで動いてるパッケージです
* UniTaskDevelop
  * UniTaskで動いてるパッケージの開発途中データです
  * 基本的にCoroutineより新しいデータです
* Coroutine
  * Coroutineで動いてるパッケージです
* CoroutineDevelop
  * Coroutineで動いてるパッケージの開発途中データです
* main
  * 始めに見る部分です
  * データは入っていません
  * ほぼReadMe用です

### 今後の開発予定
* Fadeが使いづらいなと感じているのでジェネリックの完全排除
  * Enumが大きくなるが、使う時いちいち<Enum>をつけるほうが面倒で、忘れると一瞬エラー原因もわからないため
