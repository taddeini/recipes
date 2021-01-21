const app = require("./src/server/app");
const http = require("http");
const appInstance = app();

const server = http.createServer(appInstance).listen("3000", () => {
  const host = server.address().address;
  const port = server.address().port;

  console.log("Recipes listening at http://%s:%s", host, port);
});

process.on("SIGTERM", () => {
  server.close(() => {
    process.exit(0);
  });
});
