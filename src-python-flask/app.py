from flask import Flask, request, jsonify, Response

app = Flask("my_api")

@app.route('/hello', methods=['GET', 'POST'])
def hello():
    if request.method == 'POST':
        data = request.json
        return jsonify(message=f"Received POST data: {data}")
    elif request.method == 'GET':
        r = Response()
        r.status = 200
        r.set_data("{\"message\": \"hi! Thanks for the get request!\"}")
        r.headers["Content-Type"] = "application/json"
        return r

app.run(host='0.0.0.0', port=5000)