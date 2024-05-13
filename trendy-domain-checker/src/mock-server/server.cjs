const jsonServer = require('json-server');
const server = jsonServer.create();
const router = jsonServer.router('src/mock-server/db.json');
const middlewares = jsonServer.defaults();

server.use(middlewares);


server.use((req, res, next) => {
  const _send = res.send
  res.send = function (body) {
    try {
      const json = JSON.parse(body)
      if (Array.isArray(json)) {
        if (json.length === 1) {
          return _send.call(this, JSON.stringify(json[0]))
        } else if (json.length === 0) {
          return _send.call(this, '{}', 404)
        }
      }
    } catch (e) {}
    return _send.call(this, body)
  }
  next()
})

server.use(router);
server.listen(9000, () => {
  console.log('Mock server is running on port 9000');
});