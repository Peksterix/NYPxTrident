const express = require('express');
const bodyParser = require('body-parser');
const Dictionary = require('./Dictionary');
const ShortUniqueId = require('short-unique-id');

const shortUniqueId = new ShortUniqueId({length : 5});

function OnRouterError(error, res)
{
    var msg = error.msg ? error.msg : "InvalidSyntax";
    var status = error.status ? error.status : 400;
    res.status(status).send(msg);
}

var jsonParser = bodyParser.json();

var codeToNetworkAddress = new Dictionary.Dictionary();
const app = express();

app.use(function (req, res, next) {
        
    // Website you wish to allow to connect
    res.setHeader('Access-Control-Allow-Origin', '*');
    
    // Request methods you wish to allow
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
    
    // Request headers you wish to allow
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
    
    // Pass to next layer of middleware
    next();
});


app.post('/RegisterServer', jsonParser,
/**
 * 
 * @param { NetworkAddress : string, GameMode : number } req 
 * @param { Code : string } res 
 */
function (req, res)
{
    try
    {
        var networkAddress = req.body.NetworkAddress;
        var gameMode = req.body.GameMode;
        if (networkAddress)
        {
            var code = shortUniqueId();
            codeToNetworkAddress.add(code, { NetworkAddress: networkAddress, GameMode: gameMode });
            res.status(200).send({ Code : code });
        }
        else throw({ status: 400, msg: "InvalidPayload" });
    }
    catch(e)
    {
        OnRouterError({ status: e.status, msg: e.msg }, res);
    }
});

app.post('/CodeToServer', jsonParser,
/**
 * 
 * @param { Code : string, GameMode : number } req 
 * @param { NetworkAddress : string } res 
 */
function (req, res)
{
    try
    {
        var code = req.body.Code;
        var gameMode = req.body.GameMode;
        if (code !== undefined && gameMode !== undefined && typeof code === "string" && typeof gameMode === "number")
        {
            var serverData = codeToNetworkAddress.getValue(code);
            if (serverData && serverData.GameMode === gameMode) res.status(200).send({ NetworkAddress : serverData.NetworkAddress });
            else if (serverData && serverData.GameMode !== gameMode) throw({ status: 400, msg: "GameModeMismatch" });
            else throw({ status: 404, msg: "DoesNotExist" });
        }
        else throw({ status: 400, msg: "InvalidPayload" });
    }
    catch(e)
    {
        OnRouterError({ status: e.status, msg: e.msg }, res);
    }
});

app.listen(1337, function () {
    console.log('Example app listening on port 1337.');
});