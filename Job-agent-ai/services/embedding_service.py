from sentence_transformers import SentenceTransformer, util

model = SentenceTransformer('all-MiniLM-L6-v2')


def get_embedding(text: str):
    return model.encode(text).tolist()


def calculate_similarity(text1: str, text2: str):
    emb1 = get_embedding(text1)
    emb2 = get_embedding(text2)
    score = util.cos_sim(emb1, emb2)
    return float(score.item())
