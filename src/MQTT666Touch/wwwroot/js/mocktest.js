// mock.js
const mqtt = require('mqtt')
const Mock = require('mockjs')
const { Random } = require('mockjs')

const EMQX_SERVER = 'mqtt://192.168.16.221:1883'
const CLIENT_NUM = 1  // 模拟采集
const STEP = 1 // 模拟采集时间间隔 ms
const AWAIT = 1 // 每次发送完后休眠时间，防止消息速率过快 ms
const CLIENT_POOL = []

startMock()

//设置连接后暂停时间
function sleep(timer = 1) {
  return new Promise(resolve => {
    setTimeout(resolve, timer)
  })
}

async function startMock() {
  const now = Date.now()
  for (let i = 0; i < CLIENT_NUM; i++) {
    const client = await createClient(`mock_client_${i}`)
    CLIENT_POOL.push(client)
  }
  // last 24h every 5s
  const last = 24 * 3600 * 1000
  for (let ts = now - last; ts <= now; ts += STEP) {
    for (const client of CLIENT_POOL) {
      const mockData = generateMockData()
      const data = {
        ...mockData,
        id: client.clientId,
        device_id: client.options.clientId,
        area: 0,
        ts,
      }
      mockTopic = "deivce/"  + client.options.clientId  + "/checkItems";
      client.publish(mockTopic, JSON.stringify(data))
    }
    const dateStr = new Date(ts).toLocaleTimeString()
    console.log(`${dateStr} send success.`)
    await sleep(AWAIT)
  }
  console.log(`Done, use ${(Date.now() - now) / 1000}s`)
}

/**
 * Init a virtual mqtt client
 * @param {string} clientId ClientID
 */
function createClient(clientId) {
  return new Promise((resolve, reject) => {
    const client = mqtt.connect(EMQX_SERVER, {
      clientId,
    })
    client.on('connect', () => {
      console.log(`client ${clientId} connected`)
      resolve(client)
    })
    client.on('reconnect', () => {
      console.log('reconnect')
    })
    client.on('error', (e) => {
      console.error(e)
      reject(e)
    })
  })
}

/**
* Generate mock data
*/
function generateMockData() {
 return {
  ts: Date.now(),
  product_id: 123,
  model_id: 174,
  good_result: Mock.Random.integer(0, 1), //随机生成一个整数，0/1
  bad_result: Mock.Random.integer(0, 1), //随机生成一个整数，0/1
  exception_result: Mock.Random.integer(0, 1), //随机生成一个整数，0/1
  note: "sadas",
  save_path:
    "/Upload/Image/" + 123 + "/" + Date.now() + ".png",
  d1: Mock.Random.integer(0, 1),
  d2: Mock.Random.integer(0, 1),
  d3: Mock.Random.integer(0, 1),
  d4: Mock.Random.integer(0, 1),
  d5: Mock.Random.integer(0, 1),
  d6: Mock.Random.integer(0, 1),
  d7: Mock.Random.integer(0, 1),
  d8: Mock.Random.integer(0, 1),
  d9: Mock.Random.integer(0, 1),
  d10: Mock.Random.integer(0, 1),
  d11: Mock.Random.integer(0, 1),
  d12: Mock.Random.integer(0, 1),
  d13: Mock.Random.integer(0, 1),
  d14: Mock.Random.integer(0, 1),
  d15: Mock.Random.integer(0, 1),
  d16: Mock.Random.integer(0, 1),
  d17: Mock.Random.integer(0, 1),
  d18: Mock.Random.integer(0, 1),
  d19: Mock.Random.integer(0, 1),
  d20: Mock.Random.integer(0, 1),
 }
}
