import fs from "fs";
import path from "path";

export function getAllRecipes() {
  const recipeDataPath = path.join(process.cwd(), "data/all-recipes.json");
  return JSON.parse(fs.readFileSync(recipeDataPath, "utf8"));
}

export function getRecipeByUrlTitle(urlTitle) {
  return getAllRecipes().find((recipe) => recipe.url_title === urlTitle);
}
