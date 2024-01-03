import styles from "./loading.module.scss";

const LoadingPage = () => (
  <div className={styles.container}>
    <span className={styles.text}>Trending App</span>
    <div className={styles.square}></div>
  </div>
);

export default LoadingPage;
