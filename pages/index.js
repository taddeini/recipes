import Layout from "../components/layout";
import styles from "../styles/Home.module.scss";
import { getAllRecipes } from "../lib/api/recipe-repository";
import { useState } from "react";

export async function getStaticProps() {
  const recipes = getAllRecipes();

  return {
    props: {
      recipes,
    },
  };
}

export default function Home({ recipes }) {
  const [filteredRecipes, setFilteredRecipes] = useState(recipes);

  const getTitleMatches = (term) => {
    return recipes.filter((recipe) =>
      recipe.title.toUpperCase().includes(term.toUpperCase()),
    );
  };

  const getIngredientsMatches = (term) => {
    const matches = [];
    recipes.forEach((recipe) => {
      recipe.ingredients.forEach((ingredient) => {
        if (ingredient.toUpperCase().includes(term.toUpperCase())) {
          matches.push(recipe);
        }
      });
    });

    return matches;
  };

  const onSearch = (evtArgs) => {
    const searchTerm = evtArgs.target.value;
    const matchesByTitle = getTitleMatches(searchTerm);
    const matchesByIngredients = getIngredientsMatches(searchTerm);
    const uniqueMatches = new Set([...matchesByTitle, ...matchesByIngredients]);
    setFilteredRecipes(Array.from(uniqueMatches));
  };

  return (
    <Layout>
      <div className={styles.search_container}>
        <label htmlFor="search">search by title or ingredient</label>
        <input
          id="search"
          className={styles.search_input}
          type="search"
          placeholder="search by title or ingredient"
          onChange={onSearch}
        />
      </div>
      <ul>
        {filteredRecipes.map((recipe, index) => {
          const { url_title, title } = recipe;
          return (
            <li key={`recipe-${index}`}>
              <a href={`/${url_title}`}>{title}</a>
            </li>
          );
        })}
      </ul>
    </Layout>
  );
}
