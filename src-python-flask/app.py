from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/hello', methods=['GET', 'POST'])
def hello():
    if request.method == 'POST':
        data = request.json
        return jsonify(message=f"Received POST data: {data}")
    return jsonify(message="Hello, world! This is a GET response.")

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)