from pydantic import BaseModel
from agents.resume_agent import parse_resume
from services.embedding_service import calculate_similarity
from services.parser_service import extract_text
from services.embedding_service import get_embedding
from fastapi import FastAPI
import os

app = FastAPI()


class ResumeRequest(BaseModel):
    path: str


class EmbeddingRequest(BaseModel):
    text: str


@app.get("/health")
def health():
    return {"status": "AI service running"}


@app.post("/parse-resume")
def parse_resume_api(request: ResumeRequest):
    try:
        abs_path = os.path.join(os.getcwd(), request.path)
        result = parse_resume(abs_path)
        return result
    except Exception as e:
        return {"error": str(e)}


@app.post("/parse-resumeexists")
def parse_resumeexists(data: dict):
    file_path = data["path"]

    if not os.path.exists(file_path):
        return {"error": "File not found"}

    return {"message": "File exists"}


class MatchRequest(BaseModel):
    resume_path: str
    job_description: str


@app.post("/match-job")
def match_resume(request: MatchRequest):
    resume_text = extract_text(request.resume_path)

    similarity = calculate_similarity(resume_text, request.job_description)

    return {"similarity_score": similarity}


@app.post("/generate-embedding")
def generate_embedding(req: EmbeddingRequest):
    try:
        embedding = get_embedding(req.text)
        return {"embedding": embedding}
    except Exception as e:
        return {"error": str(e)}

    # asdasdsdsdddss
