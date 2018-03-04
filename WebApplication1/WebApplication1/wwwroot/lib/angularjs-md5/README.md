# Angularjs MD5的实现

### bower

```
bower install angularjs-md5 --save
```

### 使用

```javascript
angular.module('myApp', ['angularjs-md5'])
    .controller('testController', function ($md5) {
        var hash = $md5.md5("value"); // "2063c1608d6e0baf80249c42e2be5804"
        var hmac = $md5.md5("value", "key"); // "01433efd5f16327ea4b31144572c67f6"
    });
```

## API

#### 1. MD5

```javascript
var hash = $md5.md5("value"); // "2063c1608d6e0baf80249c42e2be5804"
```
#### 2. HMAC-MD5

```javascript
var hmac = $md5.md5("value", "key"); // "01433efd5f16327ea4b31144572c67f6"
```



**HMAC-MD5比MD5多一个key，你想知道什么是HMAC-MD5?**

> 比如你和对方共享了一个密钥K，现在你要发消息给对方，既要保证消息没有被篡改，又要能证明信息确实是你本人发的，那么就把原信息和使用K计算的HMAC的值一起发过去。对方接到之后，使用自己手中的K把消息计算一下HMAC，如果和你发送的HMAC一致，那么可以认为这个消息既没有被篡改也没有冒充。