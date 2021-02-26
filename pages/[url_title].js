import Head from "next/head";
import Layout from "../components/layout";
import styles from "../styles/RecipeDetail.module.scss";
import {
  getAllRecipes,
  getRecipeByUrlTitle,
} from "../lib/api/recipe-repository";

export async function getStaticProps({ params }) {
  const recipe = getRecipeByUrlTitle(params.url_title);

  return {
    props: {
      recipe,
    },
  };
}

export async function getStaticPaths() {
  const recipes = getAllRecipes();
  const paths = recipes.map((recipe) => ({
    params: {
      url_title: recipe.url_title,
    },
  }));

  return { paths, fallback: false };
}

export default function RecipeDetail({ recipe }) {
  const {
    title,
    url_title,
    servings,
    description,
    ingredients,
    directions,
  } = recipe;

  return (
    <Layout>
      <Head>
        <title>Recipes - {url_title}</title>
      </Head>
      <section>
        <h1>{title}</h1>
        {servings !== 0 && <p className={styles.servings}>Serves {servings}</p>}
        {description && <p>{description}</p>}
      </section>

      <section>
        <h2>Ingredients</h2>
        <ul>
          {ingredients.map((ingredient, index) => (
            <li key={index}>{ingredient}</li>
          ))}
        </ul>
      </section>
      <section>
        <h2>Directions</h2>
        <ol>
          {directions.map((directionStep, index) => (
            <li key={index}>{directionStep}</li>
          ))}
        </ol>
      </section>
    </Layout>
  );
}
