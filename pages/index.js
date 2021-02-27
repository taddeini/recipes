import Layout from "../components/layout";
import styles from "../styles/Home.module.scss";
import { getAllRecipes } from "../lib/api/recipe-repository";

export async function getStaticProps() {
  const recipes = getAllRecipes();

  return {
    props: {
      recipes,
    },
  };
}

export default function Home({ recipes }) {
  return (
    <Layout>
      <div className={styles.search_container}>
        <label htmlFor="search">search by title or ingredient</label>
        <input
          id="search"
          className={styles.search_input}
          type="search"
          placeholder="search by title or ingredient"
        />
      </div>
      <ul>
        {recipes.map((recipe, index) => {
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
