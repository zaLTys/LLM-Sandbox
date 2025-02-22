# LLM-Sandbox

Run ollama container locally
```
docker run -d -v ollama_data:/root/.ollama -p 11434:11434 --name ollama ollama/ollama:latest
```
![image](https://github.com/user-attachments/assets/ec8d835e-bfd3-4637-ad6a-fda8488e7839)


Then exec into it and pull llama3 model
```
docker exec -it ollama ollama pull llama3
```
![image](https://github.com/user-attachments/assets/9b2e4083-c0e4-4e8f-83c5-7335ab51a73c)

http://localhost:11434/ should return "Ollama is running"
