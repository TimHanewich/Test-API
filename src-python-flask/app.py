from flask import Flask, request, Response
import json

app = Flask("my_api")

@app.route('/hello', methods=["GET", "POST"])
def hello():
    if request.method.upper() == 'GET':
        r = Response()
        r.status = 200
        r.set_data("{\"message\": \"hi! Thanks for the GET request!\"}")
        r.headers["Content-Type"] = "application/json"
        return r
    elif request.method.upper() == 'POST':
        data:str = request.get_data(as_text=True)
        r = Response()
        r.status = 200
        r.set_data("I got this from your POST request: '" + data + "'")
        r.headers["Content-Type"] = "text/plain"
        return r
    

_data:list[str] = []
@app.route("/data", methods=["GET", "POST", "DELETE"])
def data() -> Response:
    if request.method.upper() == "GET":
        r = Response()
        r.status = 200
        r.headers["Content-Type"] = "application/json"
        r.set_data(json.dumps(_data))
        return r
    elif request.method.upper() == "POST":

        # get the body
        body:str = request.get_data(as_text=True)
        _data.append(body)

        # return successful
        r = Response()
        r.status = 201
        return r
    elif request.method.upper() == "DELETE":

        # get the body
        body:str = request.get_data(as_text=True)

        # is inside?
        if body not in _data:
            r = Response()
            r.status = 404
            r.headers["Content-Type"] = "plain/text"
            r.set_data("Could not find data point '" + body + "' in array.")
            return r

        # remove
        _data.remove(body)

        # return success
        r = Response()
        r.status = 200
        return r

    
    

app.run(host='0.0.0.0', port=443)