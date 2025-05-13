from flask import Flask, request, jsonify, Response

app = Flask("my_api")

@app.route('/hello', methods=['GET', 'POST'])
def hello():
    if request.method == 'POST':
        data:str = request.get_data(as_text=True)
        r = Response()
        r.status = 200
        r.set_data("I got this from you: '" + data + "'")
        r.headers["Content-Type"] = "text/plain"
        return r
    elif request.method == 'GET':
        r = Response()
        r.status = 200
        r.set_data("{\"message\": \"hi! Thanks for the get request!\"}")
        r.headers["Content-Type"] = "application/json"
        return r

app.run(host='0.0.0.0', port=5000)