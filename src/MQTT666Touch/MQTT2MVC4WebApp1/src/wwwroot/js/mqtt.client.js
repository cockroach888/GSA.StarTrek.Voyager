"use strict"

function onSendMessage() {
    var topic = $('#txtTopic').val()
    var message = $('#txtMessage').val()

    console.log(`发送 - Topic: ${topic}, Message: ${message}`)
}


var connection = new signalR.HubConnectionBuilder().withUrl("/mqttHub").build()

//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true

connection.on("ReceiveMessage", function (topic, message) {
    console.log(`Topic: ${topic}, Message: ${message}`)

    var li = document.createElement("li")
    document.getElementById("msgBox").appendChild(li)
    li.textContent = `收到 - Topic: ${topic}, Message: ${message}`
})

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false
    console.log('服务端连接成功。')
}).catch(function (err) {
    return console.error(err.toString())
})

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value
//    var message = document.getElementById("messageInput").value
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString())
//    })
//    event.preventDefault()
//})


