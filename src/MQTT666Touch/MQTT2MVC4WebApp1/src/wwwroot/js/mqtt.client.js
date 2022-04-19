"use strict"

var count = 0
var connection


async function onSendMessage() {
    const topic = $('#txtTopic').val()
    const message = $('#txtMessage').val()

    console.log(`发送 - Topic: ${topic}, Message: ${message}`)

    try {
        await connection.invoke("SendMessage", topic, message)
    } catch (err) {
        console.error(err)
    }
}

function clearItems() {
    $('#msgBox').empty()
}

async function start() {
    try {
        await connection.start()
        console.log("服务端连接成功。")
    } catch (err) {
        console.error(err)
        setTimeout(start, 5000)
    }
}

$(async function () {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/mqttHub", {
            transport: signalR.HttpTransportType.LongPolling
        })
        .configureLogging(logging => {
            logging.SetMinimumLevel(LogLevel.Debug)
            logging.AddConsole()
        })
        .withAutomaticReconnect()
        .build()

    connection.on("ReceiveMessage", function (topic, message) {
        //console.log(`Topic: ${topic}, Message: ${message}`)
        count++

        $('#msgBox').prepend(`<li>收到 - Topic: ${topic}, Message: ${message}</li>`)
        $('#lblCount').text(count)
    })

    connection.onclose(async error => {
        console.log('服务端关闭连接。')

        console.assert(connection.state === signalR.HubConnectionState.Disconnected)
        console.error(`Connection closed due to error "${error}". Try refreshing this page to restart the connection.`)

        //await start()
    })

    //connection.start().then(function () {
    //    console.log('服务端连接成功。')
    //}).catch(function (err) {
    //    return console.error(err.toString())
    //})


    setInterval(clearItems, 10000)

    // 保持选项卡活动状态
    var lockResolver
    if (navigator && navigator.locks && navigator.locks.request) {
        const promise = new Promise((res) => {
            lockResolver = res
        })

        navigator.locks.request('unique_lock_name', { mode: "shared" }, () => {
            return promise
        })
    }

    await start()
})
