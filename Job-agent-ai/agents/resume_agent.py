import re

from services.parser_service import extract_text


def parse_resume(path: str):
    text = extract_text(path).lower()

    # Skills
    skills_list = ["c#", ".net", "angular",
                   "sql", "azure", "python", "java", "react"]
    skills = [skill for skill in skills_list if skill in text]

    # Experience
    exp_matches = re.findall(r"(\d+)\s+years", text)
    experience = max([int(x) for x in exp_matches], default=0)

    # Education
    education_keywords = ["bachelor", "master", "phd", "mba"]
    education = [edu for edu in education_keywords if edu in text]

    return {
        "skills": skills,
        "total_experience": experience,
        "education": education
    }
