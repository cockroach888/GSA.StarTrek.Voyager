"use strict"

function onSendMessage() {
    const topic = $('#txtTopic').val()
    const message = $('#txtMessage').val()

    console.log(`发送 - Topic: ${topic}, Message: ${message}`)
}

function clearItems() {
    $('#msgBox').empty()
}


var count = 0;
$(function () {
    const connection = new signalR.HubConnectionBuilder().withUrl("/mqttHub").build()

    connection.on("ReceiveMessage", function (topic, message) {
        //console.log(`Topic: ${topic}, Message: ${message}`)
        count++

        $('#msgBox').prepend(`<li>收到 - Topic: ${topic}, Message: ${message}</li>`)
        $('#lblCount').text(count)
    })

    connection.start().then(function () {
        console.log('服务端连接成功。')
    }).catch(function (err) {
        return console.error(err.toString())
    })
      

    setInterval(clearItems, 10000);
})
